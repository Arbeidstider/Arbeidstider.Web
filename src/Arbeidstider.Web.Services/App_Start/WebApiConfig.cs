using System.Web.Http;
using Arbeidstider.Web.Framework;
using Autofac.Integration.WebApi;

namespace Arbeidstider.Web.Services.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var resolver = new AutofacWebApiDependencyResolver(IoC.BaseContainer);

            config.DependencyResolver = resolver;
        }
    }
}
