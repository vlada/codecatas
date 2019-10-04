using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Twitter;
using Nancy;
using Owin;
using PluralsightOwinDemo.Middleware;

namespace PluralsightOwinDemo
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {


            //app.UseDebugMiddleware(new DebugMiddlewareOptions()
            //{
            //    OnIncomingRequest = (ctx) =>
            //    {
            //        var watch = new Stopwatch();
            //        watch.Start();
            //        ctx.Environment["DebugStopwatch"] = watch;
            //    },
            //    OnOutgoingRequest = (ctx) =>
            //    {
            //        var watch = (Stopwatch)ctx.Environment["DebugStopwatch"];
            //        watch.Stop();
            //        Debug.WriteLine("request took: " + watch.ElapsedMilliseconds);
            //    }
            //});

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/auth/login")
            });

            app.UseFacebookAuthentication(new FacebookAuthenticationOptions()
            {
                AppId = "692505107930444",
                AppSecret = "a3dbe4f180db90ae4bcda2df7e6b7bde",
                SignInAsAuthenticationType = "ApplicationCookie"
            });

            app.UseTwitterAuthentication(new TwitterAuthenticationOptions()
            {
                ConsumerKey = "y3mDwylbE4dFx2ZFEXkNI3jQV",
                ConsumerSecret = "caG04wlbWfm10P055q4B6gWw58Dy6iECCg2miHTSlK0PQ1oPxm",
                SignInAsAuthenticationType = "ApplicationCookie",
                BackchannelCertificateValidator = null
            });  
                 

            app.Use(async (ctx, next) =>
            {
                if (ctx.Authentication.User.Identity.IsAuthenticated)
                {
                    Debug.WriteLine("user: " + ctx.Authentication.User.Identity.Name);
                }
                else
                {
                    Debug.WriteLine("user is not authenticated");
                }

                await next();
            });

            app.UseNancy(options =>
                {
                    options.PerformPassThrough = ctx => ctx.Response.StatusCode == HttpStatusCode.NotFound;
                });

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            app.UseWebApi(config);

            //app.Use(async (ctx, next) =>
            //{
            //    await ctx.Response.WriteAsync("<html><head></head><body>hello world</body></html>");
            //});
        }
    }
}