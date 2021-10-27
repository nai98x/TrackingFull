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
    public class PuntoDeControlController : Controller
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

        //GET: PuntoDeControl
        [Route("api/PuntosDeControl?idTrayecto={id}")]
        public async Task<ActionResult> Index(string id)
        {
            Session["IdTrayecto"] = id;
            List<PuntoDeControl> lista = new List<PuntoDeControl>();
            List<PuntoDeControlModel> aux = new List<PuntoDeControlModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                HttpResponseMessage Res = await client.GetAsync("api/PuntosDeControl?idTrayecto=" + id);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<List<PuntoDeControl>>(EmpResponse);
                }
                foreach (PuntoDeControl pdc in lista)
                {
                    aux.Add(new PuntoDeControlModel()
                    {
                        Id = pdc.Id,
                        Nombre = pdc.Nombre,
                        Posicion = pdc.Posicion,
                        Agencia = new Agencia()
                        {
                            Nombre = await GetNombreAgencia(pdc.Id)
                        }
                    }); 
                }
                aux.Sort((x, y) => x.Posicion.CompareTo(y.Posicion));
                return View(aux);
            }
        }

        // GET: PuntoDeControl/Details/5
        public async Task<ActionResult> Details(int id)
        {
            ViewBag.IdTrayecto = Session["IdTrayecto"];

            using (var client = new HttpClient())
            {
                PuntoDeControl pdc = null;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/puntodecontrol/" + id;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    pdc = JsonConvert.DeserializeObject<PuntoDeControl>(EmpResponse);
                }
                if (pdc != null)
                {
                    return View(new PuntoDeControlModel()
                    {
                        Id = pdc.Id,
                        Nombre = pdc.Nombre,
                        Posicion = pdc.Posicion
                    });
                }
                return View("NotFound");
            }
        }

        // GET: PuntoDeControl/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.IdTrayecto = Session["IdTrayecto"];

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
                    aux.Add(new AgenciaModel()
                    {
                        Id = ag.Id,
                        Direccion = ag.Direccion,
                        Nombre = ag.Nombre
                    });
                }
            }

            ViewBag.Agencias = lista;

            List<PuntoDeControl> listaPdc = new List<PuntoDeControl>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                HttpResponseMessage Res = await client.GetAsync("api/PuntosDeControl?idTrayecto=" + Session["IdTrayecto"]);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    listaPdc = JsonConvert.DeserializeObject<List<PuntoDeControl>>(EmpResponse);
                }
                int mayor = 0;
                listaPdc.ForEach(x => {
                    if (x.Posicion > mayor)
                    {
                        mayor = x.Posicion;
                    }
                });

                Session["MayorPosPermitido"] = mayor + 1;
            }

            return View();
        }

        // POST: PuntoDeControl/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            int mayorpos = int.Parse(Session["MayorPosPermitido"].ToString());
            int pos = int.Parse(collection["Posicion"]);
            if (pos > 0 && pos <= mayorpos)
            {
                try
                {
                    string json =
                        "{" +
                          "'Nombre': '" + collection["Nombre"] + "'," +
                          "'Posicion': '" + collection["Posicion"] + "'," +
                          "'Agencia': {" +
                          "   'Id': '" + collection["Agencia"] + "'" +
                          " }," +
                          "'Trayecto': {" +
                          "   'Id': '" + Session["IdTrayecto"] + "'" +
                          " }" +
                        "}";
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);

                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                        string postUri = Baseurl + "api/puntodecontrol";
                        var response = await client.PostAsync(
                            postUri,
                            new StringContent(json, Encoding.UTF8, "application/json")
                        );
                        return RedirectToAction("Index", "PuntoDeControl", new { id = Session["IdTrayecto"] });
                    }
                }
                catch
                {
                    return RedirectToAction("Index", "PuntoDeControl", new { id = Session["IdTrayecto"] });
                }
            }
            else
            {
                ModelState.AddModelError("Posicion", "Debes ingresar una posicion válida");

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
                        aux.Add(new AgenciaModel()
                        {
                            Id = ag.Id,
                            Direccion = ag.Direccion,
                            Nombre = ag.Nombre
                        });
                    }
                }

                ViewBag.Agencias = lista;

                return View(new PuntoDeControlModel()
                {
                    Nombre = collection["Nombre"],
                    Posicion = int.Parse(collection["Posicion"]),
                    Agencia = new Agencia()
                    {
                        Id = int.Parse(collection["Agencia"])
                    },
                    Trayecto = new Trayecto()
                    {
                        Id = int.Parse(Session["IdTrayecto"].ToString())
                    }
                });
            }
        }

        // GET: PuntoDeControl/Edit/5
        [HttpGet]
        [Route("api/PuntoDeControl/Details/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.IdTrayecto = Session["IdTrayecto"];

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
                    aux.Add(new AgenciaModel()
                    {
                        Id = ag.Id,
                        Direccion = ag.Direccion,
                        Nombre = ag.Nombre
                    });
                }
            }

            ViewBag.Agencias = lista;

            List<PuntoDeControl> listaPdc = new List<PuntoDeControl>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                HttpResponseMessage Res = await client.GetAsync("api/PuntosDeControl?idTrayecto=" + Session["IdTrayecto"]);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    listaPdc = JsonConvert.DeserializeObject<List<PuntoDeControl>>(EmpResponse);
                }
                int mayor = 0;
                listaPdc.ForEach(x => {
                    if (x.Posicion > mayor)
                    {
                        mayor = x.Posicion;
                    }
                });

                Session["MayorPosPermitido"] = mayor + 1;
            }

            using (var client = new HttpClient())
            {
                PuntoDeControl pdc = null;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/puntodecontrol/" + id;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    pdc = JsonConvert.DeserializeObject<PuntoDeControl>(EmpResponse);
                }
                if (pdc != null)
                {
                    return View(new PuntoDeControlModel()
                    {
                        Id = pdc.Id,
                        Nombre = pdc.Nombre,
                        Posicion = pdc.Posicion
                    });
                }
                return View("NotFound");
            }
        }

        // POST: PuntoDeControl/Edit/5
        [HttpPost]
        [Route("api/PuntoDeControl/Edit/{id}")]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            int mayorpos = int.Parse(Session["MayorPosPermitido"].ToString());
            int pos = int.Parse(collection["Posicion"]);
            if (pos > 0 && pos <= mayorpos)
            {
                try
                {
                    string json =
                        "{" +
                          "'Id': '" + collection["Id"] + "'," +
                          "'Nombre': '" + collection["Nombre"] + "'," +
                          "'Posicion': '" + collection["Posicion"] + "'," +
                          "'Agencia': {" +
                          "   'Id': '" + collection["Agencia"] + "'" +
                          " }," +
                          "'Trayecto': {" +
                          "   'Id': '" + Session["IdTrayecto"] + "'" +
                          " }" +
                        "}";
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);

                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                        string editUri = Baseurl + "api/puntodecontrol/" + id;
                        var response = await client.PutAsync(
                            editUri,
                            new StringContent(json, Encoding.UTF8, "application/json")
                        );
                        return RedirectToAction("Index", "PuntoDeControl", new { id = Session["IdTrayecto"] });
                    }
                }
                catch
                {
                    return RedirectToAction("Index", "PuntoDeControl", new { id = Session["IdTrayecto"] });
                }
            }
            else
            {
                ModelState.AddModelError("Posicion", "Debes ingresar una posicion válida");

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
                        aux.Add(new AgenciaModel()
                        {
                            Id = ag.Id,
                            Direccion = ag.Direccion,
                            Nombre = ag.Nombre
                        });
                    }
                }

                ViewBag.Agencias = lista;

                return View(new PuntoDeControlModel()
                {
                    Nombre = collection["Nombre"],
                    Posicion = int.Parse(collection["Posicion"]),
                    Agencia = new Agencia()
                    {
                        Id = int.Parse(collection["Agencia"])
                    },
                    Trayecto = new Trayecto()
                    {
                        Id = int.Parse(Session["IdTrayecto"].ToString())
                    }
                });
            }
        }

        // GET: PuntoDeControl/Delete/5
        [HttpGet]
        [Route("api/PuntoDeControl/Details/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            ViewBag.IdTrayecto = Session["IdTrayecto"];
            using (var client = new HttpClient())
            {
                PuntoDeControl pdc = null;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/puntodecontrol/" + id;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    pdc = JsonConvert.DeserializeObject<PuntoDeControl>(EmpResponse);
                }
                if (pdc != null)
                {
                    return View(new PuntoDeControlModel()
                    {
                        Id = pdc.Id,
                        Nombre = pdc.Nombre,
                        Posicion = pdc.Posicion
                    });
                }
                return View("NotFound");
            }
        }

        // POST: PuntoDeControl/Delete/5
        [HttpPost]
        [Route("api/PuntoDeControl/Delete/{id}")]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl + "api/");

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                    var response = await client.DeleteAsync("puntodecontrol/" + id.ToString() + "?idTr=" + Session["IdTrayecto"]);
                    return RedirectToAction("Index", "PuntoDeControl", new { id = Session["IdTrayecto"] });
                }
            }
            catch
            {
                return RedirectToAction("Index", "PuntoDeControl", new { id = Session["IdTrayecto"] });
            }
        }
    }
}