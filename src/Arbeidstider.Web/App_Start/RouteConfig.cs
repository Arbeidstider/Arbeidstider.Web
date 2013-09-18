using System.Web.Mvc;
using System.Web.Routing;

namespace Arbeidstider.Web.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "login",
                url: "login",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Logoff",
                url: "logoff",
                defaults: new { controller = "Account", action = "LogOff", id = UrlParameter.Optional }
            );

            //routes.Add("Dashboard", new DomainRoute("mine.arbeidstider.no", "index", new { controller = "Dashboard", action = "Index"}));
            routes.Add("Dashboard", new DomainRoute("mine.arbeidstider.no", "{action}/{id}", new { controller = "Dashboard", action = "Index"}));

            routes.MapRoute(
                name: "LandingPage",
                url: "index",
                defaults: new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}