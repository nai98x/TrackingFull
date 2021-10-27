using BusinessLogicLayer;
using Shared.Entities;
using Shared.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [Authorize]
    public class UsuarioController : ApiController
    {
        IBLUsuarios blHandler = new BLUsuarios();
        IBLCasosDeUso blHandlerCU = new BLCasosDeUso();

        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            return blHandler.getAllUsuarios();
        }

        [HttpGet]
        public Usuario Get(int id)
        {
            return blHandler.getUsuario(id);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]Usuario usu)
        {
            if (ModelState.IsValid)
            {
                blHandler.AddUsuario(usu);
                return Ok(usu);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]Usuario usu)
        {
            if (ModelState.IsValid)
            {
                Usuario validar = blHandler.getUsuario(usu.Id);
                if (validar != null)
                {
                    blHandler.UpdateUsuario(usu);
                    return Ok(usu);
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
            Usuario usu = blHandler.getUsuario(id);
            if (usu != null)
            {
                blHandler.DeleteUsuario(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }


    }
}
