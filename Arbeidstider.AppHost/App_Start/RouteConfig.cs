using System.Web.Mvc;
using System.Web.Routing;

namespace Arbeidstider.AppHost.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("api/{*pathInfo}"); 
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            //routes.MapRoute(
                //name: "Login",
                //url: "login",
                //defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "ForgotPassword",
            //    url: "forgot-password",
            //    defaults: new { controller = "Home", action = "ForgotPassword", id = UrlParameter.Optional }
            //////);

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}