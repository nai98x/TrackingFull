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
    public class PaquetesEnviadosController : Controller
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

        // GET: PaquetesEnviados
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
                HttpResponseMessage Res = await client.GetAsync("api/paquetesenviadosusuario?id=" + idUsu);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<List<Paquete>>(EmpResponse);
                }
                foreach (Paquete paq in lista)
                {
                    Paquete p1 = paq;
                    aux.Add(new PaqueteModel()
                    {
                        Id = paq.Id,
                        Descripcion = paq.Descripcion,
                        FechaEntrega = paq.FechaEntrega,
                        FechaIngreso = paq.FechaIngreso,
                        Destinatario = paq.Destinatario,
                        Estado = await GetEstadoPaquete(paq.Id)
                    });
                }
                return View(aux);
            }
        }
    }
}