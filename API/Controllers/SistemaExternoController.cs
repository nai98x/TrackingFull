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
    public class SistemaExternoController : ApiController
    {
        IBLCasosDeUso blHandler = new BLCasosDeUso();
        IBLAgencias blHandlerAg = new BLAgencias();
        IBLPaquetes blHandlerPaq = new BLPaquetes();
        IBLUsuarios blHandlerUsu = new BLUsuarios();
        IBLTrayectos blHandlerTr = new BLTrayectos();

        [HttpPost]
        [Route("api/SistemaExterno/Paquete/Agregar/")]
        public IHttpActionResult AgregarPaquete([FromBody]Paquete paq)
        {
            if(paq != null && paq.Trayecto != null && paq.Remitente != null && paq.Destinatario != null && paq.Trayecto.Origen != null && paq.Trayecto.Destino != null)
            {
                Trayecto tr = paq.Trayecto;
                Usuario remitente = paq.Remitente;
                Usuario destinatario = paq.Destinatario;

                Trayecto trByCod = blHandlerTr.GetTrayectoByCodigoExterno(tr.CodigoExterno);
                Agencia origen = blHandlerAg.GetAgenciaByCodigoExterno(tr.Origen.CodigoExterno);
                Agencia destino = blHandlerAg.GetAgenciaByCodigoExterno(tr.Destino.CodigoExterno);
                Usuario rte = blHandlerUsu.GetUsuarioByCodigoExterno(remitente.CodigoExterno);
                Usuario dest = blHandlerUsu.GetUsuarioByCodigoExterno(destinatario.CodigoExterno);

                if(origen == null)
                {
                    blHandlerAg.AddAgencia(new Agencia() { 
                        Nombre = tr.Origen.Nombre,
                        CodigoExterno = tr.Origen.CodigoExterno,
                        Direccion = tr.Origen.Direccion,
                        EntregaDomicilio = tr.Origen.EntregaDomicilio
                    });
                    origen = blHandlerAg.GetAgenciaByCodigoExterno(tr.Origen.CodigoExterno);
                }
                if (destino == null)
                {
                    blHandlerAg.AddAgencia(new Agencia()
                    {
                        Nombre = tr.Destino.Nombre,
                        CodigoExterno = tr.Destino.CodigoExterno,
                        Direccion = tr.Destino.Direccion,
                        EntregaDomicilio = tr.Destino.EntregaDomicilio
                    });
                    destino = blHandlerAg.GetAgenciaByCodigoExterno(tr.Destino.CodigoExterno);
                }
                if (trByCod == null)
                {
                    blHandlerTr.AddTrayecto(new Trayecto()
                    {
                        Nombre = tr.Nombre,
                        CodigoExterno = tr.CodigoExterno,
                        Origen = origen,
                        Destino = destino
                    });
                    trByCod = blHandlerTr.GetTrayectoByCodigoExterno(tr.CodigoExterno);
                }
                if(rte == null)
                {
                    blHandlerUsu.AddUsuario(new Usuario()
                    {
                        Email = remitente.Email,
                        Password = remitente.Password,
                        CodigoExterno = remitente.CodigoExterno,
                        Rol = "Cliente",
                        Nombre = remitente.Nombre,
                        Direccion = remitente.Direccion,
                        Telefono = remitente.Telefono,
                        TipoDocumento = remitente.TipoDocumento,
                        NroDocumento = remitente.NroDocumento
                    });
                    rte = blHandlerUsu.GetUsuarioByCodigoExterno(remitente.CodigoExterno);
                }
                if (dest == null)
                {
                    blHandlerUsu.AddUsuario(new Usuario()
                    {
                        Email = destinatario.Email,
                        Password = destinatario.Password,
                        CodigoExterno = destinatario.CodigoExterno,
                        Rol = "Cliente",
                        Nombre = destinatario.Nombre,
                        Direccion = destinatario.Direccion,
                        Telefono = destinatario.Telefono,
                        TipoDocumento = destinatario.TipoDocumento,
                        NroDocumento = destinatario.NroDocumento
                    });
                    dest = blHandlerUsu.GetUsuarioByCodigoExterno(destinatario.CodigoExterno);
                }

                Paquete nuevo = new Paquete()
                {
                    CodigoExterno = paq.CodigoExterno,
                    Descripcion = paq.Descripcion,
                    FechaEntrega = paq.FechaEntrega,
                    Trayecto = trByCod,
                    Remitente = rte,
                    Destinatario = dest,
                    Entregado = false,
                    FechaIngreso = DateTime.Now
                };
                blHandlerPaq.AddPaquete(nuevo);
                return Ok(paq);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/SistemaExterno/Paquete/AvanzarEstado/")]
        public IHttpActionResult AvanzarEstadoPaquete([FromUri]string codigo)
        {
            Paquete paq = blHandlerPaq.GetPaqueteByCodigoExterno(codigo);
            if (paq != null)
            {
                bool ok = blHandler.AvanzarEstadoPaquete(paq.Id);
                if (ok)
                {
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

        [HttpPut]
        [Route("api/SistemaExterno/Paquete/RetrocederEstado/")]
        public IHttpActionResult RetrocederEstadoPaquete([FromUri]string codigo)
        {
            Paquete paq = blHandlerPaq.GetPaqueteByCodigoExterno(codigo);
            if (paq != null)
            {
                bool ok = blHandler.RetrocederEstadoPaquete(paq.Id);
                if (ok)
                {
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

        [HttpPut]
        [Route("api/SistemaExterno/Paquete/FinalizarEntrega/")]
        public IHttpActionResult FinalizarEntregaPaquete([FromUri]string codigoExterno, [FromUri]string codigo)
        {
            Paquete paq = blHandlerPaq.GetPaqueteByCodigoExterno(codigoExterno);
            if(paq != null)
            {
                bool ok = blHandler.FinalizarEntregaPaquete(paq.Id, codigo);
                if (ok)
                {
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
    }
}
