using System;
using System.Web.Routing;
using Arbeidstider.AppHost.App_Start;

namespace Arbeidstider.AppHost
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest(object src, EventArgs e)
        {
            if (Request.IsLocal)
                ServiceStack.MiniProfiler.Profiler.Start();
        }

        protected void Application_EndRequest(object src, EventArgs e)
        {
            ServiceStack.MiniProfiler.Profiler.Stop();
        }
    }
}