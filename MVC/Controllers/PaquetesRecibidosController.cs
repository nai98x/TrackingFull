using MVC.Models;
using MVC.Models.Entities;
using Newtonsoft.Json;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class PaquetesRecibidosController : Controller
    {
        private readonly string Baseurl = ConfigurationManager.AppSettings["baseurl"];

        private async Task<string> GetEstadoPaquete(int idPaq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/estadopaquete?id=" + idPaq;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<String>(EmpResponse);
                }
                return "Error";
            }
        }

        private async Task<int> GetIdUsuarioByEmail()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/GetIdUsuarioByEmail?email=" + User.Identity.Name;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<int>(EmpResponse);
                }
                return 0;
            }
        }

        private async Task<bool> GetPuedeCambiarHoraDeEntrega(int idPaq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/paquete/puedecambiarhoradeentrega?id=" + idPaq;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<bool>(EmpResponse);
                }
                return false;
            }
        }

        // GET: PaquetesRecibidos
        public async Task<ActionResult> Index()
        {
            List<Paquete> lista = new List<Paquete>();
            List<PaqueteModel> aux = new List<PaqueteModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                int idUsu = await GetIdUsuarioByEmail();
                HttpResponseMessage Res = await client.GetAsync("api/paquetesrecibidosusuario?id=" + idUsu);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<List<Paquete>>(EmpResponse);
                }
                foreach (Paquete paq in lista)
                {
                    aux.Add(new PaqueteModel()
                    {
                        Id = paq.Id,
                        Descripcion = paq.Descripcion,
                        FechaEntrega = paq.FechaEntrega,
                        FechaIngreso = paq.FechaIngreso,
                        Remitente = paq.Remitente,
                        Estado = await GetEstadoPaquete(paq.Id),
                        Codigo = paq.Codigo,
                        PuedeCambiarHoraDeEntrega = await GetPuedeCambiarHoraDeEntrega(paq.Id)
                    });
                }
                return View(aux);
            }
        }

        // GET: Paquete/Edit/5
        [HttpGet]
        [Route("api/Paquete/gethoradeentrega=id={id}")]
        public async Task<ActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                int HoraDeEntrega = 999;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/paquete/GetHoraDeEntrega?id=" + id;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    HoraDeEntrega = JsonConvert.DeserializeObject<int>(EmpResponse);
                    if(HoraDeEntrega == -1)
                    {
                        HoraDeEntrega = 0;
                    }
                }
                if (HoraDeEntrega != 999)
                {
                    return View(new HoraDeEntregaModel() {
                        Id = id,
                        HoraDeEntrega = HoraDeEntrega
                    });
                }
                return View("NotFound");
            }
        }

        // POST: Paquete/Edit/5
        [HttpPost]
        [Route("api/Paquete/updatehoradeentrega=id={id}")]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            try
            {
                string json = collection["HoraDeEntrega"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                    string editUri = Baseurl + "api/paquete/updatehoradeentrega?id=" + id;
                    var response = await client.PutAsync(
                        editUri,
                        new StringContent(json, Encoding.UTF8, "application/json")
                    );
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }
    }
}