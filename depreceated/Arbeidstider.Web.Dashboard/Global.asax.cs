using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Arbeidstider.Web.Dashboard.App_Start;
using Arbeidstider.Web.Framework;

namespace Arbeidstider.Web.Dashboard
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Initialize();
        }

        private static void Initialize()
        {
            IoC.Initialize();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }
    }
}