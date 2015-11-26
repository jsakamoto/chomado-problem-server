using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ChomadProblemServer.Startup))]

namespace ChomadProblemServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            app.UseWebApi(config);
        }
    }
}
