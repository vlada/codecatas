using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Owin;
using Nancy.Security;

namespace PluralsightOwinDemo.Modules
{
    public class NancyDemoModule : NancyModule
    {
        public NancyDemoModule()
        {

            this.RequiresAuthentication();
            Get("/nancy",  x =>
            {
                var env = Context.GetOwinEnvironment();

                var user = Context.CurrentUser;
                return "hello from nancy:  " + env["owin.RequestPathBase"] + env["owin.RequestPath"] + "<br/><br/><br/>" + " user: " + Context.CurrentUser.Identity.Name;
            });
        }
    }
}