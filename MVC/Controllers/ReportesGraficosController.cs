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
    [Authorize(Roles = "Administrador,Funcionario")]
    public class ReportesGraficosController : Controller
    {
        private readonly string Baseurl = ConfigurationManager.AppSettings["baseurl"];

        public async Task<ActionResult> Index()
        {
            int RecibidoEnOrigen = 0;
            int EsperandoEnOrigen = 0;
            int EnViaje = 0;
            int RecibidoEnDestino = 0;
            int EntregadoAlCliente = 0;

            int[] resultado;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/graficas/PorEstado";
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    resultado = JsonConvert.DeserializeObject<int[]>(EmpResponse);
                    RecibidoEnOrigen = resultado[0];
                    EsperandoEnOrigen = resultado[1];
                    EnViaje = resultado[2];
                    RecibidoEnDestino = resultado[3];
                    EntregadoAlCliente = resultado[4];
                }
            }
            ViewBag.RecibidoEnOrigen = RecibidoEnOrigen;
            ViewBag.EsperandoEnOrigen = EsperandoEnOrigen;
            ViewBag.EnViaje = EnViaje;
            ViewBag.RecibidoEnDestino = RecibidoEnDestino;
            ViewBag.EntregadoAlCliente = EntregadoAlCliente;


            int administradores = 0;
            int funcionarios = 0;
            int clientes = 0;

            int[] resultado2;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token"].Value);

                string apiuri = "api/graficas/RolesUsuarios";
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    resultado2 = JsonConvert.DeserializeObject<int[]>(EmpResponse);
                    administradores = resultado2[0];
                    funcionarios = resultado2[1];
                    clientes = resultado2[2];
                }
            }
            ViewBag.Administradores = administradores;
            ViewBag.Funcionarios = funcionarios;
            ViewBag.Clientes = clientes;

            return View();
        }
    }
}