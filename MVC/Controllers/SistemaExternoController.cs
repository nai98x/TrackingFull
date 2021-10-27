using MVC.Models;
using Newtonsoft.Json;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Configuration;
using MVC.Models.Entities;

namespace MVC.Controllers
{
    [Authorize(Roles = "Superadministrador")]
    public class SistemaExternoController : Controller
    {
        private readonly string Baseurl = ConfigurationManager.AppSettings["baseurl"];

        // GET: Sistema externo
        public async Task<ActionResult> Index()
        {
            List<Usuario> temp = new List<Usuario>();
            List<SistemaExternoModel> usuarios = new List<SistemaExternoModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                HttpResponseMessage Res = await client.GetAsync("api/usuario");

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    temp = JsonConvert.DeserializeObject<List<Usuario>>(EmpResponse);
                    foreach (Usuario usu in temp)
                    {
                        if (usu.Rol == "Sistema Externo")
                        {
                            usuarios.Add(new SistemaExternoModel()
                            {
                                Id = usu.Id,
                                Email = usu.Email,
                                Password = usu.Password,
                                Nombre = usu.Nombre
                            });
                        }
                    }
                }
                return View(usuarios);
            }
        }

        // GET: Sistema externo/Details/5
        public async Task<ActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                Usuario adm = null;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/usuario/" + id;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    adm = JsonConvert.DeserializeObject<Usuario>(EmpResponse);
                }
                if (adm != null)
                {
                    if (adm.Rol == "Sistema Externo")
                    {
                        return View(new SistemaExternoModel()
                        {
                            Id = adm.Id,
                            Email = adm.Email,
                            Password = adm.Password,
                            Nombre = adm.Nombre
                        });
                    }
                }
                return View("NotFound");
            }
        }

        // GET: Sistema externo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sistema externo/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                string json =
                    "{" +
                      "'Email': '" + collection["Email"] + "'," +
                      "'Password': '" + collection["Password"] + "'," +
                      "'Rol': 'Sistema Externo'," +
                      "'Nombre': '" + collection["Nombre"] + "'" +
                    "}";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                    string postUri = Baseurl + "api/SistemaExterno/agregar";
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

        // GET: Sistema externo/Edit/5
        [HttpGet]
        [Route("api/Usuario/Details/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                Usuario adm = null;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/usuario/" + id;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    adm = JsonConvert.DeserializeObject<Usuario>(EmpResponse);
                }
                if (adm != null)
                {
                    if (adm.Rol == "Sistema Externo")
                    {
                        return View(new SistemaExternoModel()
                        {
                            Id = adm.Id,
                            Email = adm.Email,
                            Password = adm.Password,
                            Nombre = adm.Nombre
                        });
                    }
                }
                return View("NotFound");
            }
        }

        // POST: Sistema externo/Edit/5
        [HttpPost]
        [Route("api/Usuario/Edit/{id}")]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            try
            {
                string json =
                    "{" +
                      "'Id': " + id + "," +
                      "'Email': '" + collection["Email"] + "'," +
                      "'Password': '" + collection["Password"] + "'," +
                      "'Rol': 'Sistema Externo'," +
                      "'Nombre': '" + collection["Nombre"] + "'" +
                    "}";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                    string editUri = Baseurl + "api/usuario/" + id;
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

        // GET: Sistema externo/Delete/5
        [HttpGet]
        [Route("api/Usuario/Details/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                Usuario adm = null;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/usuario/" + id;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    adm = JsonConvert.DeserializeObject<Usuario>(EmpResponse);
                }
                if (adm != null)
                {
                    if (adm.Rol == "Sistema Externo")
                    {
                        return View(new SistemaExternoModel()
                        {
                            Id = adm.Id,
                            Email = adm.Email,
                            Password = adm.Password,
                            Nombre = adm.Nombre
                        });
                    }
                }
                return View("NotFound");
            }
        }

        // POST: Sistema externo/Delete/5
        [HttpPost]
        [Route("api/Usuario/Delete/{id}")]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl + "api/");

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                    var response = await client.DeleteAsync("usuario/" + id.ToString());
                    var result = response.StatusCode;
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