using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogicLayer;
using Shared.Entities;

namespace API.Controllers
{
    [Authorize]
    public class AuxiliarController : ApiController
    {
        IBLCasosDeUso blHandler = new BLCasosDeUso();
        IBLAgencias blHandlerAG = new BLAgencias();
        IBLPaquetes blHandlerPaq = new BLPaquetes();
        IBLUsuarios blHandlerUsu = new BLUsuarios();
        IBLTrayectos blHandlerTr = new BLTrayectos();

        [HttpPost]
        [Route("api/Cliente/AgregarPaquete/{id}")]
        public void AgregarPaquete([FromUri]int id, [FromBody]Paquete paq)
        {
            blHandlerUsu.AgregarPaquete(paq);
        }
    }
}
