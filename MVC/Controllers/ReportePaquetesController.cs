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
    [Authorize(Roles = "Funcionario")]
    public class ReportePaquetesController : Controller
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

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<Usuario> lista = new List<Usuario>();
            List<Usuario> aux = new List<Usuario>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                HttpResponseMessage Res = await client.GetAsync("api/clientes");

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<Usuario>>(EmpResponse);
                }
            }
            lista.Add(new Usuario() { 
                Id = 999,
                Nombre = "Ninguno"
            });
            lista.AddRange(aux);
            List<Usuario> aux2 = new List<Usuario>();
            foreach (Usuario usu in lista)
            {
                if(usu.CodigoExterno == null)
                {
                    aux2.Add(usu);
                }
            }

            ViewBag.Remitentes = aux2;
            ViewBag.Destinatarios = aux2;
            ViewBag.Estados = new string[6] {"Ninguno", "Recibido en origen", "Esperando en origen", "En viaje", "Recibido en Destino", "Entregado al Cliente" };
            var a = new ReportePaquetesModel();
            a.FechaDesde = DateTime.Now.AddMonths(-1);
            a.FechaHasta = DateTime.Now;
            return View(a);
        }

        [HttpPost]
        public async Task<ActionResult> Index(FormCollection collection)
        {
            List<Paquete> lista = new List<Paquete>();
            List<PaqueteModel> aux = new List<PaqueteModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string estado = collection["Estado"];
                string estadobuenito= "";
                if(estado != "Ninguno")
                {
                    estadobuenito = estado;
                }
                string nuevaFechaD;
                string nuevaFechaH;
                if(collection["FechaDesde"] == null || collection["FechaDesde"] == "")
                {
                    nuevaFechaD = DateTime.Now.AddYears(14).Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                }
                else
                {
                    nuevaFechaD = collection["FechaDesde"];
                }
                if (collection["FechaHasta"] == null || collection["FechaHasta"] == "")
                {
                    nuevaFechaH = DateTime.Now.AddYears(14).Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                }
                else
                {
                    nuevaFechaH = collection["FechaHasta"];
                }
                string uri = "api/Paquete/Filtro?fechaDesde=" + nuevaFechaD + "&fechaHasta=" + nuevaFechaH + "&estado=" + estadobuenito + "&idDestinatario=" + collection["Destinatario"] + "&idRemitente=" + collection["Remitente"];
                HttpResponseMessage Res = await client.GetAsync(uri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<List<Paquete>>(EmpResponse);
                }
                foreach (Paquete paq in lista)
                {
                    if(paq.CodigoExterno == null)
                    {
                        int HoraDeEntrega = 999;
                        using (var client2 = new HttpClient())
                        {

                            client2.BaseAddress = new Uri(Baseurl);

                            client2.DefaultRequestHeaders.Clear();
                            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                            string apiuri2 = "api/paquete/GetHoraDeEntrega?id=" + paq.Id;
                            HttpResponseMessage Res2 = await client2.GetAsync(apiuri2);

                            if (Res2.IsSuccessStatusCode)
                            {
                                var EmpResponse2 = Res2.Content.ReadAsStringAsync().Result;
                                HoraDeEntrega = JsonConvert.DeserializeObject<int>(EmpResponse2);
                            }
                        }
                        string hde;
                        if (HoraDeEntrega == 999)
                        {
                            hde = "ERROR";
                        }
                        else if (HoraDeEntrega == -1)
                        {
                            hde = "(No tiene)";
                        }
                        else
                        {
                            hde = HoraDeEntrega.ToString();
                        }
                        aux.Add(new PaqueteModel()
                        {
                            Id = paq.Id,
                            Descripcion = paq.Descripcion,
                            FechaEntrega = paq.FechaEntrega,
                            FechaIngreso = paq.FechaIngreso,
                            Remitente = paq.Remitente,
                            Destinatario = paq.Destinatario,
                            Estado = await GetEstadoPaquete(paq.Id),
                            HoraDeEntrega = hde
                        });
                    }
                }
            }

            return View("GetReportePaquetes", aux);
        }

        [HttpGet]
        [Route("api/Paquete/GetReportePaquetes/")]
        public ActionResult GetReportePaquetes(IEnumerable<PaqueteModel> paquetes)
        {
            return View(paquetes);
        }
    }
}