using BusinessLogicLayer;
using Shared.Entities;
using System.Collections.Generic;
using System.Web.Http;

namespace API.Controllers
{
    [Authorize]
    public class PuntoDeControlController : ApiController
    {
        IBLPuntosDeControl blHandler = new BLPuntosDeControl();

        [HttpGet]
        public IEnumerable<PuntoDeControl> Get()
        {
            return blHandler.GetAllPuntosDeControl();
        }

        [HttpGet]
        public PuntoDeControl Get(int id)
        {
            return blHandler.GetPuntoDeControl(id);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]PuntoDeControl pdc)
        {
            blHandler.AddPuntoDeControl(pdc);
            return Ok(pdc);
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]PuntoDeControl pdc)
        {
            if (ModelState.IsValid)
            {
                if (pdc != null)
                {
                    blHandler.UpdatePuntoDeControl(pdc);
                    return Ok(pdc);
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
        public IHttpActionResult Delete(int id, [FromUri]int idTr)
        {
            PuntoDeControl pdc = blHandler.GetPuntoDeControl(id);
            if (pdc != null)
            {
                blHandler.DeletePuntoDeControl(id, idTr);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
