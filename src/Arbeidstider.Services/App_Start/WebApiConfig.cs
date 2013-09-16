using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Arbeidstider.Services.DelegatingHandlers.Token;

namespace Arbeidstider.Services.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Create and instance of TokenInspector setting the default inner handler
            //TokenInspector tokenInspector = new TokenInspector() { InnerHandler = new HttpControllerDispatcher(config) };

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "services/TimesheetService/GetAllTimesheets",
                defaults: new { id = RouteParameter.Optional, controller = "Timesheet", action = "GetAllTimesheets", employerID = 0, startDate = "null", endDate = "null" }
            );

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}
