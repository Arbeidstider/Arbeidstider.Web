using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Arbeidstider.Web.App_Start;
using Arbeidstider.Web.Helpers;

namespace Arbeidstider.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (Context.Request.Url.ToString().ToLower().Contains("assets")) return;
            if (Context.IsLoggedIn()) return;
            if (Context.Request.Url.ToString().ToLower().EndsWith("login")) return;
            Context.Response.RedirectToRoute("login");
        }
    }
}