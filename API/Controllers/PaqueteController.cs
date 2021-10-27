using BusinessLogicLayer;
using Shared.Entities;
using System.Collections.Generic;
using System.Web.Http;

namespace API.Controllers
{
    [Authorize]
    public class PaqueteController : ApiController
    {
        IBLPaquetes blHandler = new BLPaquetes();

        [HttpGet]
        public IEnumerable<Paquete> Get()
        {
            return blHandler.GetAllPaquetes();
        }

        [HttpGet]
        public Paquete Get(int id)
        {
            return blHandler.GetPaquete(id);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]Paquete paq)
        {
            blHandler.AddPaquete(paq);
            return Ok(paq);
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]Paquete paq)
        {
            if (ModelState.IsValid)
            {
                if (paq != null)
                {
                    blHandler.UpdatePaquete(paq);
                    return Ok(paq);
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
            Paquete paq = blHandler.GetPaquete(id);
            if (paq != null)
            {
                blHandler.DeletePaquete(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
