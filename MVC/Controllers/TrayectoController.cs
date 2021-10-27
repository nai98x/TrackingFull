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
    [Authorize(Roles = "Administrador")]
    public class TrayectoController : Controller
    {
        private readonly string Baseurl = ConfigurationManager.AppSettings["baseurl"];

        private async Task<string> GetNombreAgencia(int idPdc)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/nomagdepdc?id=" + idPdc;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<String>(EmpResponse);
                }
                return "Error";
            }
        }

        // GET: Trayecto
        public async Task<ActionResult> Index()
        {
            List<Trayecto> lista = new List<Trayecto>();
            List<TrayectoModel> aux = new List<TrayectoModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                HttpResponseMessage Res = await client.GetAsync("api/trayecto");

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<List<Trayecto>>(EmpResponse);
                }
                foreach (Trayecto tr in lista)
                {
                    if(tr.CodigoExterno == null)
                    {
                        aux.Add(new TrayectoModel()
                        {
                            Id = tr.Id,
                            Nombre = tr.Nombre,
                            Origen = tr.Origen,
                            Destino = tr.Destino
                        });
                    }
                }
                return View(aux);
            }
        }

        // GET: Trayecto/Details/5
        public async Task<ActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                Trayecto tr = null;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/trayecto/" + id;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    tr = JsonConvert.DeserializeObject<Trayecto>(EmpResponse);
                }
                if (tr != null)
                {
                    return View(new TrayectoModel()
                    {
                        Id = tr.Id,
                        Nombre = tr.Nombre,
                        Origen = tr.Origen,
                        Destino = tr.Destino
                    });
                }
                return View("NotFound");
            }
        }

        // GET: Trayecto/Create
        public async Task<ActionResult> Create()
        {
            List<Agencia> lista = new List<Agencia>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                HttpResponseMessage Res = await client.GetAsync("api/agencia");

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<List<Agencia>>(EmpResponse);
                }
            }

            ViewBag.AgenciasOrigen = lista;
            ViewBag.AgenciasDestino = lista;

            return View();
        }

        // POST: Trayecto/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                string json =
                    "{" +
                      "'Nombre': '" + collection["Nombre"] + "'," +
                      "'Origen': {" +
                      "  'Id': '" + collection["Origen"] + "'" +
                      "}," +
                      "'Destino': {" +
                      "  'Id': '" + collection["Destino"] + "'" +
                      "}" +
                    "}";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                    string postUri = Baseurl + "api/trayecto";
                    var response = await client.PostAsync(
                        postUri,
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

        // GET: Trayecto/Edit/5
        [HttpGet]
        [Route("api/Trayecto/Details/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            List<Agencia> lista = new List<Agencia>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                HttpResponseMessage Res = await client.GetAsync("api/agencia");

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<List<Agencia>>(EmpResponse);
                }
            }

            ViewBag.AgenciasOrigen = lista;
            ViewBag.AgenciasDestino = lista;

            using (var client = new HttpClient())
            {
                Trayecto tr = null;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/trayecto/" + id;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    tr = JsonConvert.DeserializeObject<Trayecto>(EmpResponse);
                }
                if (tr != null)
                {
                    return View(new TrayectoModel()
                    {
                        Id = tr.Id,
                        Nombre = tr.Nombre,
                        Origen = tr.Origen,
                        Destino = tr.Destino
                    });
                }
                return View("NotFound");
            }
        }

        // POST: Trayecto/Edit/5
        [HttpPost]
        [Route("api/Trayecto/Edit/{id}")]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            try
            {
                string json =
                    "{" +
                      "'Id': '" + collection["Id"] + "'," +
                      "'Nombre': '" + collection["Nombre"] + "'," +
                      "'Origen': {" +
                      "  'Id': '" + collection["Origen"] + "'" +
                      "}," +
                      "'Destino': {" +
                      "  'Id': '" + collection["Destino"] + "'" +
                      "}" +
                    "}";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                    string editUri = Baseurl + "api/trayecto/" + id;
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

        // GET: Trayecto/Delete/5
        [HttpGet]
        [Route("api/Trayecto/Details/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                Trayecto tr = null;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/trayecto/" + id;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    tr = JsonConvert.DeserializeObject<Trayecto>(EmpResponse);
                }
                if (tr != null)
                {
                    return View(new TrayectoModel()
                    {
                        Id = tr.Id,
                        Nombre = tr.Nombre,
                        Origen = tr.Origen,
                        Destino = tr.Destino
                    });
                }
                return View("NotFound");
            }
        }

        // POST: Trayecto/Delete/5
        [HttpPost]
        [Route("api/Trayecto/Delete/{id}")]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl + "api/");

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                    var response = await client.DeleteAsync("trayecto/" + id.ToString());
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