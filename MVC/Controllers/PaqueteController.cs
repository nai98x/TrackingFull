using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MVC.Models.Entities;
using MVC.Report;
using Newtonsoft.Json;
using QRCoder;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC.Controllers
{
    [Authorize(Roles = "Funcionario")]
    public class PaqueteController : Controller
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

        // GET: Paquete
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

                HttpResponseMessage Res = await client.GetAsync("api/paquete");

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
                return View(aux);
            }
        }

        // GET: Paquete/Details/5
        public async Task<ActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                Paquete paq = null;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/paquete/" + id;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    paq = JsonConvert.DeserializeObject<Paquete>(EmpResponse);
                }
                if (paq != null)
                {
                    return View(new PaqueteModel()
                    {
                        Id = paq.Id,
                        Descripcion = paq.Descripcion,
                        FechaEntrega = paq.FechaEntrega
                    });
                }
                return View("NotFound");
            }
        }

        // GET: Paquete/Create
        public async Task<ActionResult> Create()
        {
            List<Usuario> lista = new List<Usuario>();

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
                    lista = JsonConvert.DeserializeObject<List<Usuario>>(EmpResponse);
                }
            }

            ViewBag.ClientesRemitentes = lista;
            ViewBag.ClientesDestinatarios = lista;

            List<Trayecto> listaTr = new List<Trayecto>();

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
                    listaTr = JsonConvert.DeserializeObject<List<Trayecto>>(EmpResponse);
                }
            }

            ViewBag.Trayectos = listaTr;

            return View();
        }

        // POST: Paquete/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                string json =
                    "{" +
                      "'Descripcion': '" + collection["Descripcion"] + "'," +
                      "'FechaEntrega': '" + collection["FechaEntrega"] + "'," +
                      "'Trayecto': {" +
                         "'Id': '" + collection["Trayecto"] + "'" +
                      "}," +
                      "'Remitente': {" +
                         "'Id': '" + collection["Remitente"] + "'" +
                      "}," +
                      "'Destinatario': {" +
                         "'Id': '" + collection["Destinatario"] + "'" +
                      "}" +
                    "}";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                    string postUri = Baseurl + "api/paquete";
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

        // GET: Paquete/Edit/5
        [HttpGet]
        [Route("api/Paquete/Details/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            List<Usuario> lista = new List<Usuario>();

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
                    lista = JsonConvert.DeserializeObject<List<Usuario>>(EmpResponse);
                }
            }

            ViewBag.ClientesRemitentes = lista;
            ViewBag.ClientesDestinatarios = lista;

            using (var client = new HttpClient())
            {
                Paquete paq = null;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/paquete/" + id;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    paq = JsonConvert.DeserializeObject<Paquete>(EmpResponse);
                }
                if (paq != null)
                {
                    return View(new PaqueteModel()
                    {
                        Id = paq.Id,
                        Descripcion = paq.Descripcion,
                        FechaEntrega = paq.FechaEntrega
                    });
                }
                return View("NotFound");
            }
        }

        // POST: Paquete/Edit/5
        [HttpPost]
        [Route("api/Paquete/Edit/{id}")]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            try
            {
                string json =
                    "{" +
                      "'Id': " + id + "," +
                      "'Descripcion': '" + collection["Descripcion"] + "'," +
                      "'FechaEntrega': '" + collection["FechaEntrega"] + "'" +
                    "}";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                    string editUri = Baseurl + "api/paquete/" + id;
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

        // GET: Paquete/Delete/5
        [HttpGet]
        [Route("api/Paquete/Details/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                Paquete paq = null;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/paquete/" + id;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    paq = JsonConvert.DeserializeObject<Paquete>(EmpResponse);
                }
                if (paq != null)
                {
                    return View(new PaqueteModel()
                    {
                        Id = paq.Id,
                        Descripcion = paq.Descripcion,
                        FechaEntrega = paq.FechaEntrega
                    });
                }
                return View("NotFound");
            }
        }

        // POST: Paquete/Delete/5
        [HttpPost]
        [Route("api/Paquete/Delete/{id}")]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl + "api/");

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                    var response = await client.DeleteAsync("paquete/" + id.ToString());
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        private System.Drawing.Image GenerateQRCode(string content, int size)
        {
            QrEncoder encoder = new QrEncoder(ErrorCorrectionLevel.Q);
            QrCode qrCode;
            encoder.TryEncode(content, out qrCode);

            GraphicsRenderer gRenderer = new GraphicsRenderer(new FixedModuleSize(4, QuietZoneModules.Two), System.Drawing.Brushes.Black, System.Drawing.Brushes.White);

            MemoryStream ms = new MemoryStream();
            gRenderer.WriteToStream(qrCode.Matrix, ImageFormat.Bmp, ms);

            var imageTemp = new Bitmap(ms);

            var image = new Bitmap(imageTemp, new System.Drawing.Size(new System.Drawing.Point(size, size)));

            return (System.Drawing.Image)image;
        }

        public async Task<ActionResult> Report(string id)
        {
            int idPaq = Int32.Parse(id);
            using (var client = new HttpClient())
            {
                string estado;
                string origen;
                string destino;
                Paquete paq = null;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/paquete/" + idPaq;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    paq = JsonConvert.DeserializeObject<Paquete>(EmpResponse);
                }
                if (paq != null)
                {
                    #region Estado del Paquete
                    using (var client2 = new HttpClient())
                    {
                        client2.BaseAddress = new Uri(Baseurl);

                        client2.DefaultRequestHeaders.Clear();
                        client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                        string apiuri2 = "api/estadopaquete?id=" + idPaq;
                        HttpResponseMessage Res2 = await client2.GetAsync(apiuri2);

                        if (Res2.IsSuccessStatusCode)
                        {
                            var EmpResponse = Res2.Content.ReadAsStringAsync().Result;
                            estado = JsonConvert.DeserializeObject<String>(EmpResponse);
                        }
                        else
                        {
                            estado = "ERROR";
                        }
                    }
                    #endregion
                    #region Origen Y Destino
                    using (var client3 = new HttpClient())
                    {
                        client3.BaseAddress = new Uri(Baseurl);

                        client3.DefaultRequestHeaders.Clear();
                        client3.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client3.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                        string apiuri3 = "api/paquete/agencias?id=" + idPaq;
                        HttpResponseMessage Res3 = await client3.GetAsync(apiuri3);

                        if (Res3.IsSuccessStatusCode)
                        {
                            var EmpResponse = Res3.Content.ReadAsStringAsync().Result;
                            string[] resp = JsonConvert.DeserializeObject<String[]>(EmpResponse);
                            origen = resp[0];
                            destino = resp[1];
                        }
                        else
                        {
                            origen = "ERROR";
                            destino = "ERROR";
                        }
                    }
                    #endregion
                    #region QR
                    QRCodeGenerator qr = new QRCodeGenerator();
                    QRCodeData data = qr.CreateQrCode(paq.Id.ToString(), QRCodeGenerator.ECCLevel.Q);
                    QRCode code = new QRCode(data);

                    using(Bitmap bitmap = code.GetGraphic(8))
                    {
                        using(MemoryStream ms = new MemoryStream())
                        {
                            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            ViewBag.imageBytes = ms.ToArray();
                        }
                    }

                    System.Drawing.Image imgZenBarcode = GenerateQRCode(paq.Id.ToString(), 100);
                    iTextSharp.text.Image imgQRCode;
                    imgQRCode = iTextSharp.text.Image.GetInstance(imgZenBarcode, System.Drawing.Imaging.ImageFormat.Png);

                    #endregion

                    PaqueteReport paqueteReport = new PaqueteReport();
                    byte[] abytes = paqueteReport.PrepararReport(paq, estado, origen, destino, imgQRCode);
                    return File(abytes, "application/pdf");
                }
            }
            return View("NotFound");
        }
    }
}