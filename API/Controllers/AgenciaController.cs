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
    public class AgenciaController : ApiController
    {
        IBLAgencias blHandler = new BLAgencias();
        IBLCasosDeUso blHandlerCU = new BLCasosDeUso();

        [HttpGet]
        public IEnumerable<Agencia> Get()
        {
            return blHandler.GetAllAgencias();
        }

        [HttpGet]
        public Agencia Get(int id)
        {
            return blHandler.GetAgencia(id);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]Agencia ag)
        {
            if (ModelState.IsValid)
            {
                if (ag != null)
                {
                    blHandler.AddAgencia(ag);
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
        public IHttpActionResult Put([FromBody]Agencia ag)
        {
            Agencia validar = blHandler.GetAgencia(ag.Id);
            if (validar != null)
            {
                blHandler.UpdateAgencia(ag);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            Agencia ag = blHandler.GetAgencia(id);
            if (ag != null)
            {
                blHandler.DeleteAgencia(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
