using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using PluralsightOwinDemo.Models;

namespace PluralsightOwinDemo.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {
            var providers = HttpContext.GetOwinContext().Authentication
                .GetAuthenticationTypes(x => !string.IsNullOrEmpty(x.Caption)).ToList();

             
            var model = new LoginModel();
            model.AuthProviders = providers;
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (model.Username.Equals("vlado", StringComparison.OrdinalIgnoreCase) && model.Password == "password")
            {
                var identity = new ClaimsIdentity("ApplicationCookie");
                identity.AddClaims(new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, model.Username),
                    new Claim(ClaimTypes.Name, model.Username)
                });

                HttpContext.GetOwinContext().Authentication.SignIn(identity);
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }

       

        public ActionResult SocialLogin(string id)
        {

            try
            {
                HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties()
                {
                    RedirectUri = "/secret"
                }, id);

            }
            catch (Exception e)
            {
               
            }
          

            return new HttpUnauthorizedResult();
        }
    }
}