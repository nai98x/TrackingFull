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
    public class TrayectoController : ApiController
    {
        IBLTrayectos blHandler = new BLTrayectos();
        IBLCasosDeUso blHandlerCU = new BLCasosDeUso();

        [HttpGet]
        public IEnumerable<Trayecto> Get()
        {
            return blHandler.getAllTrayectos();
        }

        [HttpGet]
        public Trayecto Get(int id)
        {
            return blHandler.getTrayecto(id);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]Trayecto tr)
        {
            if (ModelState.IsValid)
            {
                if (tr != null)
                {
                    blHandler.AddTrayecto(tr);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]Trayecto tr)
        {
            if (ModelState.IsValid)
            {
                Trayecto validar = blHandler.getTrayecto(tr.Id);
                if (validar != null)
                {
                    blHandler.UpdateTrayecto(tr);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            Trayecto tr = blHandler.getTrayecto(id);
            if (tr != null)
            {
                blHandler.DeleteTrayecto(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
