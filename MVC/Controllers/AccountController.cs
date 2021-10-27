using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MVC.DAL;
using MVC.Models;
using Newtonsoft.Json;
using Shared.Entities;

namespace MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string email;
            string rol;
            string token;
            using (var client = new HttpClient())
            {
                string Baseurl = ConfigurationManager.AppSettings["baseurl"];

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string json =
                    "{" +
                      "'Username': '" + model.Email + "'," +
                      "'Password': '" + model.Password + "'," +
                    "}";

                string postUri = Baseurl + "api/login/authenticate";
                HttpResponseMessage Res = await client.PostAsync(
                    postUri,
                    new StringContent(json, Encoding.UTF8, "application/json")
                );

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    String[] response = JsonConvert.DeserializeObject<String[]>(EmpResponse);

                    rol = response[0];
                    email = model.Email;
                    token = response[1];
                }
                else
                {
                    ModelState.AddModelError("", "Email o contraseña incorrecta");
                    return View(model);
                }
            }

            FormsAuthentication.SetAuthCookie(model.Email, false);
            var authTicket = new FormsAuthenticationTicket(1, email, DateTime.Now, DateTime.Now.AddDays(1), false, rol);
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            var apiCookie = new HttpCookie("token", token);
            HttpContext.Response.Cookies.Add(authCookie);
            HttpContext.Response.Cookies.Add(apiCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Requerir que el usuario haya iniciado sesión con nombre de usuario y contraseña o inicio de sesión externo
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // El código siguiente protege de los ataques por fuerza bruta a los códigos de dos factores. 
            // Si un usuario introduce códigos incorrectos durante un intervalo especificado de tiempo, la cuenta del usuario 
            // se bloqueará durante un período de tiempo especificado. 
            // Puede configurar el bloqueo de la cuenta en IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Código no válido.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(NuevoClienteModel model)
        {
            try
            {
                string json =
                    "{" +
                      "'Email': '" + model.Email + "'," +
                      "'Password': '" + model.Password + "'," +
                      "'Rol': 'Cliente'," +
                      "'Nombre': '" + model.Nombre + "'," +
                      "'Direccion': '" + model.Direccion + "'," +
                      "'Telefono': '" + model.Telefono + "'," +
                      "'TipoDocumento': '" + model.TipoDocumento.ToString() + "'," +
                      "'NroDocumento': '" + model.NroDocumento + "'," +
                    "}";
                using (var client = new HttpClient())
                {
                    string Baseurl = ConfigurationManager.AppSettings["baseurl"];
                    client.BaseAddress = new Uri(Baseurl);
                    string postUri = Baseurl + "api/cliente/agregar";
                    var response = await client.PostAsync(
                        postUri,
                        new StringContent(json, Encoding.UTF8, "application/json")
                    );
                }

                string email;
                string rol;
                string token;
                using (var client = new HttpClient())
                {
                    string Baseurl = ConfigurationManager.AppSettings["baseurl"];

                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string json2 =
                        "{" +
                          "'Username': '" + model.Email + "'," +
                          "'Password': '" + model.Password + "'," +
                        "}";

                    string postUri = Baseurl + "api/login/authenticate";
                    HttpResponseMessage Res = await client.PostAsync(
                        postUri,
                        new StringContent(json2, Encoding.UTF8, "application/json")
                    );

                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        String[] response = JsonConvert.DeserializeObject<String[]>(EmpResponse);

                        rol = response[0];
                        email = model.Email;
                        token = response[1];
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error inesperado al iniciar sesión");
                        return View(model);
                    }
                }

                FormsAuthentication.SetAuthCookie(model.Email, false);
                var authTicket = new FormsAuthenticationTicket(1, email, DateTime.Now, DateTime.Now.AddMinutes(30), false, rol);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                var apiCookie = new HttpCookie("token", token);
                HttpContext.Response.Cookies.Add(authCookie);
                HttpContext.Response.Cookies.Add(apiCookie);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // No revelar que el usuario no existe o que no está confirmado
                    return View("ForgotPasswordConfirmation");
                }

                // Para obtener más información sobre cómo habilitar la confirmación de cuentas y el restablecimiento de contraseña, visite https://go.microsoft.com/fwlink/?LinkID=320771
                // Enviar correo electrónico con este vínculo
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Restablecer contraseña", "Para restablecer la contraseña, haga clic <a href=\"" + callbackUrl + "\">aquí</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // No revelar que el usuario no existe
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Solicitar redireccionamiento al proveedor de inicio de sesión externo
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generar el token y enviarlo
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            bool existeUser;
            using (var client = new HttpClient())
            {
                string Baseurl = ConfigurationManager.AppSettings["baseurl"];
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string apiuri = "api/usuario/ExisteUser?email=" + loginInfo.Email;
                HttpResponseMessage Res = await client.GetAsync(apiuri);

                if (Res.IsSuccessStatusCode)
                {
                    existeUser = true;
                }
                else
                {
                    existeUser = false;
                }
            }
            if (existeUser) // Login
            {
                // ReDESSOciaLES
                string email;
                string rol;
                string token;
                using (var client = new HttpClient())
                {
                    string Baseurl = ConfigurationManager.AppSettings["baseurl"];

                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string json2 =
                        "{" +
                          "'Username': '" + loginInfo.Email + "'," +
                          "'Password': 'ReDESSOciaLES'," +
                        "}";

                    string postUri = Baseurl + "api/login/authenticate";
                    HttpResponseMessage Res = await client.PostAsync(
                        postUri,
                        new StringContent(json2, Encoding.UTF8, "application/json")
                    );

                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        String[] response = JsonConvert.DeserializeObject<String[]>(EmpResponse);

                        rol = response[0];
                        email = loginInfo.Email;
                        token = response[1];
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error inesperado al iniciar sesión");
                        return View();
                    }
                }

                FormsAuthentication.SetAuthCookie(email, false);
                var authTicket = new FormsAuthenticationTicket(1, email, DateTime.Now, DateTime.Now.AddMinutes(30), false, rol);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                var apiCookie = new HttpCookie("token", token);
                HttpContext.Response.Cookies.Add(authCookie);
                HttpContext.Response.Cookies.Add(apiCookie);
                return RedirectToAction("Index", "Home");
            }
            else // Registro
            {
                ViewBag.LoginProvider = loginInfo.ExternalIdentity.Actor;
                return View("ExternalLoginConfirmation", new NuevoClienteModel { Email = loginInfo.Email, Nombre= loginInfo.ExternalIdentity.Name });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(NuevoClienteModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            try
            {
                string json =
                    "{" +
                        "'Email': '" + model.Email + "'," +
                        "'Password': 'ReDESSOciaLES'," +
                        "'Rol': 'Cliente'," +
                        "'Nombre': '" + model.Nombre + "'," +
                        "'Direccion': '" + model.Direccion + "'," +
                        "'Telefono': '" + model.Telefono + "'," +
                        "'TipoDocumento': '" + model.TipoDocumento.ToString() + "'," +
                        "'NroDocumento': '" + model.NroDocumento + "'," +
                    "}";
                using (var client = new HttpClient())
                {
                    string Baseurl = ConfigurationManager.AppSettings["baseurl"];
                    client.BaseAddress = new Uri(Baseurl);
                    string postUri = Baseurl + "api/cliente/agregar";
                    var response = await client.PostAsync(
                        postUri,
                        new StringContent(json, Encoding.UTF8, "application/json")
                    );
                }

                string email;
                string rol;
                string token;
                using (var client = new HttpClient())
                {
                    string Baseurl = ConfigurationManager.AppSettings["baseurl"];

                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string json2 =
                        "{" +
                            "'Username': '" + model.Email + "'," +
                            "'Password': 'ReDESSOciaLES'," +
                        "}";

                    string postUri = Baseurl + "api/login/authenticate";
                    HttpResponseMessage Res = await client.PostAsync(
                        postUri,
                        new StringContent(json2, Encoding.UTF8, "application/json")
                    );

                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        String[] response = JsonConvert.DeserializeObject<String[]>(EmpResponse);

                        rol = response[0];
                        email = model.Email;
                        token = response[1];
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error inesperado al iniciar sesión");
                        return View(model);
                    }
                }

                FormsAuthentication.SetAuthCookie(model.Email, false);
                var authTicket = new FormsAuthenticationTicket(1, email, DateTime.Now, DateTime.Now.AddMinutes(30), false, rol);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                var apiCookie = new HttpCookie("token", token);
                HttpContext.Response.Cookies.Add(authCookie);
                HttpContext.Response.Cookies.Add(apiCookie);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(model);
            }
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Aplicaciones auxiliares
        // Se usa para la protección XSRF al agregar inicios de sesión externos
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}