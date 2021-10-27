using BusinessLogicLayer;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [Authorize]
    public class CasosDeUsoController : ApiController
    {
        IBLCasosDeUso blHandler = new BLCasosDeUso();
        IBLAgencias blHandlerAG = new BLAgencias();
        IBLPaquetes blHandlerPaq = new BLPaquetes();
        IBLUsuarios blHandlerUsu = new BLUsuarios();
        IBLTrayectos blHandlerTr = new BLTrayectos();

        [HttpPost]
        [Route("api/Administrador/Agregar/")]
        public IHttpActionResult AsignarAdministrador([FromBody]Usuario adm)
        {
            if (adm != null)
            {
                Usuario existente = blHandlerUsu.getUsuarioByEmail(adm.Email);
                if (existente == null)
                {
                    blHandler.AsignarAdministrador(adm);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/Funcionario/Agregar")]
        public IHttpActionResult AsignarEmpleado([FromBody]Usuario emp)
        {
            if (emp != null)
            {
                Usuario existente = blHandlerUsu.getUsuarioByEmail(emp.Email);
                if (existente == null)
                {
                    blHandler.AsignarEmpleado(emp);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/SistemaExterno/Agregar")]
        public IHttpActionResult AsignarSistemaExterno([FromBody]Usuario usu)
        {
            if (usu != null)
            {
                Usuario existente = blHandlerUsu.getUsuarioByEmail(usu.Email);
                if (existente == null)
                {
                    blHandler.AsignarSistemaExterno(usu);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/Agencia/Agregar")]
        public IHttpActionResult AltaAgencia([FromBody]Agencia ag)
        {
            if (ag != null)
            {
                blHandler.AltaAgencia(ag);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("api/Agencia/Eliminar/{id}")]
        public IHttpActionResult BajaAgencia(int id)
        {
            Agencia ag = blHandlerAG.GetAgencia(id);
            if (ag != null)
            {
                blHandler.BajaAgencia(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("api/Agencia/Modificar/{id}")]
        public IHttpActionResult ModificarAgencia([FromBody]Agencia ag)
        {
            if (ag != null)
            {
                blHandler.ModificarAgencia(ag);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/Trayecto/Agregar")]
        public IHttpActionResult AltaTrayecto([FromBody]Trayecto tr)
        {
            if (tr != null)
            {
                blHandler.AltaTrayecto(tr);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("api/Trayecto/Eliminar/{id}")]
        public IHttpActionResult BajaTrayecto(int id)
        {
            Trayecto tr = blHandlerTr.getTrayecto(id);
            if (tr != null)
            {
                blHandler.BajaTrayecto(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("api/Trayecto/Modificar/{id}")]
        public IHttpActionResult ModificarTrayecto([FromBody]Trayecto tr)
        {
            if (tr != null)
            {
                blHandler.ModificarTrayecto(tr);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/Cliente/Agregar")]
        public IHttpActionResult AltaCliente([FromBody]Usuario cli)
        {
            if (cli != null)
            {
                Usuario existente = blHandlerUsu.getUsuarioByEmail(cli.Email);
                if (existente == null)
                {
                    blHandler.AltaCliente(cli);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("api/Cliente/Eliminar/{id}")]
        public IHttpActionResult BajaCliente(int id)
        {
            Usuario cli = blHandlerUsu.getUsuario(id);
            if (cli != null)
            {
                blHandler.BajaCliente(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("api/Cliente/Modificar/{id}")]
        public IHttpActionResult ModificarCliente([FromBody]Usuario cli)
        {
            if (cli != null)
            {
                blHandler.ModificarCliente(cli);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/Paquete/Agregar")]
        public IHttpActionResult AltaPaquete([FromBody]Paquete paq, [FromBody]Agencia ag)
        {
            if (paq != null)
            {
                blHandler.AltaPaquete(paq, ag);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("api/Paquete/AvanzarEstado/")]
        public IHttpActionResult AvanzarEstadoPaquete([FromUri]int id)
        {
            bool ok = blHandler.AvanzarEstadoPaquete(id);
            if (ok)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/Paquete/RetrocederEstado/")]
        public IHttpActionResult RetrocederEstadoPaquete([FromUri]int id)
        {
            bool ok = blHandler.RetrocederEstadoPaquete(id);
            if (ok)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/Paquete/FinalizarEntrega/")]
        public IHttpActionResult FinalizarEntregaPaquete([FromUri]int id, [FromUri]string codigo)
        {
            bool ok = blHandler.FinalizarEntregaPaquete(id, codigo);
            if (ok)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/Paquete/CoordinarHoraEntrega/{id}")]
        public IHttpActionResult CoordinarHoraEntregaPaquete([FromBody]Paquete paq)
        {
            if (paq != null)
            {
                blHandler.CoordinarHoraEntregaPaquete(paq);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/Usuario/IniciarSesion/")]
        public IHttpActionResult IniciarSesion([FromUri]string mail, [FromUri]string pass)
        {
            bool combCorrecta = blHandler.IniciarSesion(mail, pass);
            if (combCorrecta)
            {
                Usuario usu = blHandlerUsu.getUsuarioByEmail(mail);
                return Ok(usu);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/PuntosDeControl/")]
        public IEnumerable<PuntoDeControl> PuntosDeControlDeTrayecto([FromUri]int idTrayecto)
        {
            return blHandler.PuntosDeControlDeTrayecto(idTrayecto);
        }

        [HttpGet]
        [Route("api/nomagdepdc/")]
        public string NombreAgenciaDePuntoDeControl([FromUri]int id)
        {
            return blHandler.NombreAgenciaDePuntoDeControl(id);
        }

        [HttpGet]
        [Route("api/paquetesenviadosusuario/")]
        public IEnumerable<Paquete> GetPaquetesEnviadosUsuario([FromUri]int id)
        {
            return blHandlerUsu.GetPaquetesEnviados(id);
        }

        [HttpGet]
        [Route("api/paquetesrecibidosusuario/")]
        public IEnumerable<Paquete> GetPaquetesRecibidosUsuario([FromUri]int id)
        {
            return blHandlerUsu.GetPaquetesRecibidos(id);
        }


        [HttpGet]
        [Route("api/estadopaquete/")]
        public string GetEstadoPaquete([FromUri]int id)
        {
            return blHandler.GetEstadoPaquete(id);
        }

        [HttpGet]
        [Route("api/clientes/")]
        public IEnumerable<Usuario> Clientes()
        {
            return blHandler.GetClientes();
        }

        [HttpGet]
        [Route("api/GetIdUsuarioByEmail/")]
        public int GetIdUsuarioByEmail([FromUri]string email)
        {
            return blHandlerUsu.getUsuarioByEmail(email).Id;
        }

        [HttpGet]
        [Route("api/Paquete/Agencias/")]
        public string[] GetAgenciasPaquete([FromUri]int id)
        {
            return blHandler.NombresAgenciasOrigenDestino(id);
        }

        [HttpGet]
        [Route("api/Usuario/ExisteUser/")]
        [AllowAnonymous]
        public IHttpActionResult GetPassByEmail([FromUri]string email)
        {
            if(blHandlerUsu.getUsuarioByEmail(email) != null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("api/Paquete/Filtro/")]
        public IEnumerable<Paquete> getPaquetesFiltro([FromUri]DateTime fechaDesde, [FromUri]DateTime fechaHasta, [FromUri]string estado, [FromUri]int idDestinatario, [FromUri]int idRemitente)
        {
            return blHandler.getPaquetesFiltro(fechaDesde,fechaHasta,estado,idDestinatario,idRemitente);
        }

        [HttpGet]
        [Route("api/Paquete/GetHoraDeEntrega/")]
        public int GetHoraDeEntregaPaquete([FromUri]int id)
        {
            return blHandler.GetHoraDeEntregaPaquete(id);
        }

        [HttpPut]
        [Route("api/Paquete/UpdateHoraDeEntrega/")]
        public IHttpActionResult UpdateHoraDeEntregaPaquete([FromUri]int id, [FromBody]int hora)
        {
            bool ok = blHandler.UpdateHoraDeEntrega(id, hora);
            if (ok)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/paquete/puedecambiarhoradeentrega")]
        public bool PuedeCambiarHoraDeEntregaPaquete([FromUri]int id)
        {
            return blHandler.PuedeCambiarHoraDeEntregaPaquete(id);
        }

        [HttpPut]
        [Route("api/usuario/CambiarRol/")]
        public bool CambiarRolUsuario([FromUri]int id, [FromUri]string rol)
        {
            return blHandler.CambiarPermisos(id, rol);
        }

        [HttpGet]
        [Route("api/Graficas/PorEstado/")]
        public int[] GraficaPorEstado()
        {
            return blHandler.GraficasPorEstado();
        }

        [HttpGet]
        [Route("api/Graficas/RolesUsuarios")]
        public int[] GraficaRolesUsuarios()
        {
            return blHandler.GraficasPorUsuario();
        }
    }
}
