﻿using System.IO;
using System.Linq;
using System.Web;
using Nancy;
using Owin;


namespace Angulair.Services
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use((ctx, next) =>
            {
                var output = ctx.Get<TextWriter>("host.TraceOutput");
                output.WriteLine(
                    "{0} {1}: {2}",
                    ctx.Request.Scheme,
                    ctx.Request.Method,
                    ctx.Request.Path);
                return next();
            });
            app.MapSignalR();
            app.UseNancy(options =>
                            options.PerformPassThrough = context =>
                                context.Response.StatusCode == HttpStatusCode.NotFound);
   
            //app.Run(ctx =>
            //{
            //    ctx.Response.ContentType = "text/plain";
            //    return ctx.Response.WriteAsync("hello");
            //});

        }
    }

    //Install-Package Nancy.Owin
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get[@"/(.*)"] = p => View["index"];
            Get[@""] = p => View["index"];

            Get[@"/admin"] = p => View["admin"];
        }
    }

    //remove static middleware
}