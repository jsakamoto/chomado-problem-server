using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
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
            
            // Fix: CORS support of WebAPI doesn't work on mono. http://stackoverflow.com/questions/31590869/web-api-2-post-request-not-working-on-mono
            if (Type.GetType("Mono.Runtime") != null) config.MessageHandlers.Add(new MonoPatchingDelegatingHandler());

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

        /// <summary>
        /// Work around a bug in mono's implementation of System.Net.Http where calls to HttpRequestMessage.Headers.Host will fail unless we set it explicitly.
        /// This should be transparent and cause no side effects.
        /// </summary>
        private class MonoPatchingDelegatingHandler : DelegatingHandler
        {
            protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                request.Headers.Host = request.Headers.GetValues("Host").FirstOrDefault();
                return await base.SendAsync(request, cancellationToken);
            }
        }
    }
}
