using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Arbeidstider.Business.Logic.IoC;
using Arbeidstider.Web.Framework.Scaffolding;
using Arbeidstider.Web.Services.App_Start;

namespace Arbeidstider.Web.Services
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

            Container.Build();
            var dates = ScaffoldHelper.GetDates(new DateTime(2013, 9, 24));
            var times = ScaffoldHelper.GetTimes(new TimeSpan(8, 0, 0), new TimeSpan(16, 0, 0));

            Timesheets.Scaffold(7, dates, times);
        }

        protected void Application_Error()
        {
        }
    }
}