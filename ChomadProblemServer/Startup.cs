using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

[assembly: OwinStartup(typeof(ChomadProblemServer.Startup))]

namespace ChomadProblemServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            // Static file support.
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            app.UseStaticFiles(new StaticFileOptions
            {
                FileSystem = new PhysicalFileSystem(root: baseDir),
                ServeUnknownFileTypes = false
            });

            // ASP.NET Web API support.
            var config = new HttpConfiguration();
            config.EnableCors();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            app.UseWebApi(config);

            // NancyFx support.
            app.UseNancy();
        }
    }
}
