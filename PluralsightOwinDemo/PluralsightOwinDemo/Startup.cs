using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Owin;
using PluralsightOwinDemo.Middleware;

namespace PluralsightOwinDemo
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {


            app.UseDebugMiddleware(new DebugMiddlewareOptions()
            {
                OnIncomingRequest = (ctx) =>
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    ctx.Environment["DebugStopwatch"] = watch;
                },
                OnOutgoingRequest = (ctx) =>
                {
                    var watch = (Stopwatch)ctx.Environment["DebugStopwatch"];
                    watch.Stop();
                    Debug.WriteLine("request took: " + watch.ElapsedMilliseconds);
                }
            });
            
            app.Use(async (ctx, next) =>
            {
              await    ctx.Response.WriteAsync("<html><head></head><body>hello world</body></html>");
            });
        }
    }
}