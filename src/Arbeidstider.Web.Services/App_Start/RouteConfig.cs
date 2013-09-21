using System.Web.Mvc;
using System.Web.Routing;

namespace Arbeidstider.Web.Services.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "TimesheetService",
                url: "TimesheetService/{action}",
                defaults: new { controller = "Timesheet", action = "GetAllTimesheets"  }
            );


            ControllerBuilder.Current.DefaultNamespaces.Add("Arbeidstider.Web.Services.Controllers");
        }
    }
}