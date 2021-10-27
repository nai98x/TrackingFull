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
    [Authorize(Roles = "Administrador")]
    public class PermisosDeUsuariosController : Controller
    {
        private readonly string Baseurl = ConfigurationManager.AppSettings["baseurl"];

        public async Task<ActionResult> Index()
        {
            List<Usuario> temp = new List<Usuario>();
            List<PermisosUsuarioModel> usuarios = new List<PermisosUsuarioModel>();

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
                            usuarios.Add(new PermisosUsuarioModel()
                            {
                                Id = usu.Id,
                                Email = usu.Email,
                                Rol = usu.Rol
                            });
                        }
                    }
                    foreach (Usuario usu in temp)
                    {
                        if (usu.Rol == "Cliente")
                        {
                            usuarios.Add(new PermisosUsuarioModel()
                            {
                                Id = usu.Id,
                                Email = usu.Email,
                                Rol = usu.Rol
                            });
                        }
                    }
                }
                return View(usuarios);
            }
        }

        [HttpGet]
        [Route("api/Usuario/Details/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Roles = new string[2] { "Cliente", "Funcionario" };
            using (var client = new HttpClient())
            {
                Usuario usu = null;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/usuario/" + id;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    usu = JsonConvert.DeserializeObject<Usuario>(EmpResponse);
                }
                if (usu != null)
                {
                    if (usu.Rol == "Funcionario" || usu.Rol == "Cliente")
                    {
                        return View(new PermisosUsuarioModel()
                        {
                            Id = usu.Id,
                            Email = usu.Email,
                            Rol = usu.Rol
                        });
                    }
                }
                return View("NotFound");
            }
        }

        [HttpPost]
        [Route("api/Usuario/Edit/{id}")]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            try
            {
                string json = collection["Rol"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                    string editUri = Baseurl + "api/usuario/CambiarRol?id=" + id + "&rol=" + collection["Rol"];
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