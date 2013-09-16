using System.Web.Mvc;

namespace Arbeidstider.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Request.Url != null && HttpContext.Request.Url.Host.ToLower().Equals("mine.arbeidstider.no"))
            {
                string[] url = HttpContext.Request.Url.ToString().Replace("http://", "").Replace("https://", "").Split('/');
                string action = url[1];
                filterContext.Result = RedirectToAction(action, "Dashboard");
            }
        }
    }
}
