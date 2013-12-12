using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Arbeidstider.DataAccess.Domain;
using Arbeidstider.Web.Dashboard.Controllers;
using Arbeidstider.Web.Framework.Services;

namespace Arbeidstider.Web.Dashboard.Filters
{
    public class AdminAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var employee = EmployeeService.Instance.GetEmployee(HttpContext.Current.User.Identity.Name);
            if (employee != null && employee.IsAdmin()) return;

            RouteData routeData = new RouteData();
            routeData.Values.Add("action", "Unauthorized");
            routeData.Values.Add("controller", "Error");
            IController controller = new ErrorController();

            RequestContext requestContext = new RequestContext(new HttpContextWrapper(HttpContext.Current), routeData);

            controller.Execute(requestContext);
        }
    }
}