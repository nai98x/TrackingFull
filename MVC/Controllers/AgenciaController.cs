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
    public class AgenciaController : Controller
    {
        private readonly string Baseurl = ConfigurationManager.AppSettings["baseurl"];

        // GET: Agencia
        public async Task<ActionResult> Index()
        {
            List<Agencia> lista = new List<Agencia>();
            List<AgenciaModel> aux = new List<AgenciaModel>();

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
                foreach (Agencia ag in lista)
                {
                    if(ag.CodigoExterno == null)
                    {
                        aux.Add(new AgenciaModel()
                        {
                            Id = ag.Id,
                            Direccion = ag.Direccion,
                            Nombre = ag.Nombre,
                            EntregaDomicilio = ag.EntregaDomicilio
                        });
                    }
                }
                return View(aux);
            }
        }

        // GET: Agencia/Details/5
        public async Task<ActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                Agencia ag = null;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/agencia/" + id;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    ag = JsonConvert.DeserializeObject<Agencia>(EmpResponse);
                }
                if (ag != null)
                {
                    return View(new AgenciaModel()
                    {
                        Id = ag.Id,
                        Nombre = ag.Nombre,
                        Direccion = ag.Direccion,
                        EntregaDomicilio = ag.EntregaDomicilio
                    });
                }
                return View("NotFound");
            }
        }

        // GET: Agencia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agencia/Create
        [HttpPost]
        public async Task<ActionResult> Create(AgenciaModel model)
        {
            try
            {
                string json =
                    "{" +
                      "'Nombre': '" + model.Nombre + "'," +
                      "'Direccion': '" + model.Direccion + "'," +
                      "'EntregaDomicilio': '" + model.EntregaDomicilio + "'" +
                    "}";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                    string postUri = Baseurl + "api/agencia";
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

        // GET: Agencia/Edit/5
        [HttpGet]
        [Route("api/Agencia/Details/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                Agencia ag = null;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/agencia/" + id;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    ag = JsonConvert.DeserializeObject<Agencia>(EmpResponse);
                }
                if (ag != null)
                {
                    return View(new AgenciaModel()
                    {
                        Id = ag.Id,
                        Direccion = ag.Direccion,
                        Nombre = ag.Nombre,
                        EntregaDomicilio = ag.EntregaDomicilio
                    });
                }
                return View("NotFound");
            }
        }

        // POST: Agencia/Edit/5
        [HttpPost]
        [Route("api/Agencia/Edit/{id}")]
        public async Task<ActionResult> Edit(int id, AgenciaModel model)
        {
            try
            {
                string json =
                    "{" +
                      "'Id': " + id + "," +
                      "'Nombre': '" + model.Nombre + "'," +
                      "'Direccion': '" + model.Direccion + "'," +
                      "'EntregaDomicilio': '" + model.EntregaDomicilio + "'" +
                    "}";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                    string editUri = Baseurl + "api/agencia/" + id;
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

        // GET: Agencia/Delete/5
        [HttpGet]
        [Route("api/Agencia/Details/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                Agencia ag = null;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/agencia/" + id;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    ag = JsonConvert.DeserializeObject<Agencia>(EmpResponse);
                }
                if (ag != null)
                {
                    return View(new AgenciaModel()
                    {
                        Id = ag.Id,
                        Direccion = ag.Direccion,
                        Nombre = ag.Nombre,
                        EntregaDomicilio = ag.EntregaDomicilio
                    });
                }
                return View("NotFound");
            }
        }

        // POST: Agencia/Delete/5
        [HttpPost]
        [Route("api/Agencia/Delete/{id}")]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl + "api/");

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                    var response = await client.DeleteAsync("agencia/" + id.ToString());
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