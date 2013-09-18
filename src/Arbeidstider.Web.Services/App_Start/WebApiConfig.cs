using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Arbeidstider.Web.Services.Controllers;

namespace Arbeidstider.Web.Services.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Create and instance of TokenInspector setting the default inner handler
            //TokenInspector tokenInspector = new TokenInspector() { InnerHandler = new HttpControllerDispatcher(config) };

            config.Routes.MapHttpRoute(
                name: "GetAllTimesheets",
                routeTemplate: "TimesheetService/{action}",
                defaults: new { controller = "Timesheet", action = "GetAllTimesheets", employerID = 0, startDate = "", endDate = "" }
            );

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
            
            ControllerBuilder.Current.DefaultNamespaces.Add("Arbeidstider.Web.Services.Controllers");
        }
    }
}
