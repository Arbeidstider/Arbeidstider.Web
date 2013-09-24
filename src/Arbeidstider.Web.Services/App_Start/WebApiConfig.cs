using System.Web.Http;
using Arbeidstider.Business.Logic.IoC;
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

            Container.Build();
            var resolver = new AutofacWebApiDependencyResolver(Container.BaseContainer);

            config.DependencyResolver = resolver;
        }
    }
}
