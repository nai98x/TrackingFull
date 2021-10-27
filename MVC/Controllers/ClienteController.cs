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
    [Authorize(Roles = "Funcionario")]
    public class ClienteController : Controller
    {
        private readonly string Baseurl = ConfigurationManager.AppSettings["baseurl"];

        private TiposDocumento ObtenerTdoc(string s)
        {
            if (s == "CI")
            {
                return TiposDocumento.CI;
            }
            else if (s == "Pasaporte")
            {
                return TiposDocumento.Pasaporte;
            }
            else
            {
                return TiposDocumento.RUT;
            }
        }

        // GET: Cliente
        public async Task<ActionResult> Index()
        {
            List<Usuario> temp = new List<Usuario>();
            List<ClienteModel> usuarios = new List<ClienteModel>();

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
                        if (usu.Rol == "Cliente")
                        {
                            usuarios.Add(new ClienteModel()
                            {
                                Id = usu.Id,
                                Email = usu.Email,
                                Password = usu.Password,
                                Nombre = usu.Nombre,
                                Direccion = usu.Direccion,
                                Telefono = usu.Telefono,
                                TipoDocumento = ObtenerTdoc(usu.TipoDocumento),
                                NroDocumento = usu.NroDocumento
                            });
                        }
                    }
                }
                return View(usuarios);
            }
        }

        // GET: Cliente/Details/5
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
                    if (fun.Rol == "Cliente")
                    {
                        return View(new ClienteModel()
                        {
                            Id = fun.Id,
                            Email = fun.Email,
                            Password = fun.Password,
                            Nombre = fun.Nombre,
                            Direccion = fun.Direccion,
                            Telefono = fun.Telefono,
                            TipoDocumento = ObtenerTdoc(fun.TipoDocumento),
                            NroDocumento = fun.NroDocumento
                        });
                    }
                }
                return View("NotFound");
            }
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                string tipo= collection["TipoDocumento"];
                if(tipo == "1")
                {
                    tipo = "CI";
                }
                else if (tipo == "2")
                {
                    tipo = "Pasaporte";
                }
                else
                {
                    tipo = "RUT";
                }
                string json =
                    "{" +
                      "'Email': '" + collection["Email"] + "'," +
                      "'Password': '" + collection["Password"] + "'," +
                      "'Rol': 'Cliente'," +
                      "'Nombre': '" + collection["Nombre"] + "'," +
                      "'Direccion': '" + collection["Direccion"] + "'," +
                      "'Telefono': '" + collection["Telefono"] + "'," +
                      "'TipoDocumento': '" + tipo + "'," +
                      "'NroDocumento': '" + collection["NroDocumento"] + "'," +
                    "}";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                    string postUri = Baseurl + "api/cliente/agregar";
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

        // GET: Cliente/Edit/5
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
                    if (fun.Rol == "Cliente")
                    {
                        return View(new ClienteModel()
                        {
                            Id = fun.Id,
                            Email = fun.Email,
                            Password = fun.Password,
                            Nombre = fun.Nombre,
                            Direccion = fun.Direccion,
                            Telefono = fun.Telefono,
                            TipoDocumento = ObtenerTdoc(fun.TipoDocumento),
                            NroDocumento = fun.NroDocumento
                        });
                    }
                }
                return View("NotFound");
            }
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        [Route("api/Usuario/Edit/{id}")]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            try
            {
                string tipo = collection["TipoDocumento"];
                if (tipo == "1")
                {
                    tipo = "CI";
                }
                else if (tipo == "2")
                {
                    tipo = "Pasaporte";
                }
                else
                {
                    tipo = "RUT";
                }
                string json =
                    "{" +
                      "'Id': " + id + "," +
                      "'Email': '" + collection["Email"] + "'," +
                      "'Password': '" + collection["Password"] + "'," +
                      "'Rol': 'Cliente'," +
                      "'Nombre': '" + collection["Nombre"] + "'," +
                      "'Direccion': '" + collection["Direccion"] + "'," +
                      "'Telefono': '" + collection["Telefono"] + "'," +
                      "'TipoDocumento': '" + tipo + "'," +
                      "'NroDocumento': '" + collection["NroDocumento"] + "'," +
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

        // GET: Cliente/Delete/5
        [HttpGet]
        [Route("api/Usuario/Details/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                Usuario cli = null;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/usuario/" + id;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    cli = JsonConvert.DeserializeObject<Usuario>(EmpResponse);
                }
                if (cli != null)
                {
                    if (cli.Rol == "Cliente")
                    {
                        return View(new ClienteModel()
                        {
                            Id = cli.Id,
                            Email = cli.Email,
                            Password = cli.Password,
                            Nombre = cli.Nombre,
                            Direccion = cli.Direccion,
                            Telefono = cli.Telefono,
                            TipoDocumento = ObtenerTdoc(cli.TipoDocumento),
                            NroDocumento = cli.NroDocumento
                        });
                    }
                }
                return View("NotFound");
            }
        }

        // POST: Cliente/Delete/5
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