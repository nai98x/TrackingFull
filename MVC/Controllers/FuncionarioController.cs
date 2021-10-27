using MVC.Models;
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
    public class FuncionarioController : Controller
    {
        private readonly string Baseurl = ConfigurationManager.AppSettings["baseurl"];

        // GET: Funcionario
        public async Task<ActionResult> Index()
        {
            List<Usuario> temp = new List<Usuario>();
            List<FuncionarioModel> usuarios = new List<FuncionarioModel>();

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
                        if (usu.Rol == "Funcionario")
                        {
                            usuarios.Add(new FuncionarioModel()
                            {
                                Id = usu.Id,
                                Email = usu.Email,
                                Password = usu.Password
                            });
                        }
                    }
                }
                return View(usuarios);
            }
        }

        // GET: Funcionario/Details/5
        public async Task<ActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                Usuario fun = null;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/usuario/" + id;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    fun = JsonConvert.DeserializeObject<Usuario>(EmpResponse);
                }
                if (fun != null)
                {
                    if(fun.Rol == "Funcionario")
                    {
                        return View(new FuncionarioModel()
                        {
                            Id = fun.Id,
                            Email = fun.Email,
                            Password = fun.Password
                        });
                    }
                }
                return View("NotFound");
            }
        }

        // GET: Funcionario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Funcionario/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                string json =
                    "{" +
                      "'Email': '" + collection["Email"] + "'," +
                      "'Password': '" + collection["Password"] + "'," +
                      "'Rol': 'Funcionario'" +
                    "}";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                    string postUri = Baseurl + "api/funcionario/agregar";
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

        // GET: Funcionario/Edit/5
        [HttpGet]
        [Route("api/Usuario/Details/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                Usuario fun = null;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/usuario/" + id;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    fun = JsonConvert.DeserializeObject<Usuario>(EmpResponse);
                }
                if (fun != null)
                {
                    if(fun.Rol == "Funcionario")
                    {
                        return View(new FuncionarioModel()
                        {
                            Id = fun.Id,
                            Email = fun.Email,
                            Password = fun.Password
                        });
                    }
                }
                return View("NotFound");
            }
        }

        // POST: Funcionario/Edit/5
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
                      "'Rol': 'Funcionario'" +
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

        // GET: Funcionario/Delete/5
        [HttpGet]
        [Route("api/Usuario/Details/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                Usuario fun = null;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/usuario/" + id;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    fun = JsonConvert.DeserializeObject<Usuario>(EmpResponse);
                }
                if (fun != null)
                {
                    if(fun.Rol == "Funcionario")
                    {
                        return View(new FuncionarioModel()
                        {
                            Id = fun.Id,
                            Email = fun.Email,
                            Password = fun.Password
                        });
                    }
                }
                return View("NotFound");
            }
        }

        // POST: Funcionario/Delete/5
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
