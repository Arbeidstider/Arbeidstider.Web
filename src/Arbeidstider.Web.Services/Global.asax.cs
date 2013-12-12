using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Arbeidstider.Web.Framework;
using Arbeidstider.Web.Services.App_Start;

namespace Arbeidstider.Web.Services
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            IoC.Initialize();
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        protected void Application_Error()
        {
        }
    }
}