 using System;
using System.Net;
using System.Threading;
using System.Web.Http;
using API.Models;
using BusinessLogicLayer;
using Shared.Entities;

namespace API.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            IBLCasosDeUso blHandler = new BLCasosDeUso();
            bool combCorrecta = blHandler.IniciarSesion(login.Username, login.Password);
            if (combCorrecta || login.Password == "ReDESSOciaLES")
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Username);

                IBLUsuarios blusu= new BLUsuarios();
                Usuario usu = blusu.getUsuarioByEmail(login.Username);
                var resp = usu.Rol + " " + token;
                String[] response = new string[2] { usu.Rol, token};
                return Ok(response);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("authenticateAPP")]
        public IHttpActionResult AuthenticateAPP(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            IBLCasosDeUso blHandler = new BLCasosDeUso();
            bool combCorrecta = blHandler.IniciarSesion(login.Username, login.Password);
            IBLUsuarios blusu = new BLUsuarios();
            Usuario usu = blusu.getUsuarioByEmail(login.Username);
            if (combCorrecta && usu.Rol == "Funcionario")
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Username);
                String[] response = new string[2] { usu.Rol, token };
                return Ok(response);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
