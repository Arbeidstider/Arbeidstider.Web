using System;
using System.Web.Mvc;

namespace Arbeidstider.Web.Dashboard.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound(Exception exception, int statusCode)
        {
            Response.StatusCode = statusCode;
            return View("404");
        }

        public ActionResult Index(Exception exception, int statusCode)
        {
            Response.StatusCode = statusCode;
            return View("500");
        }

        public ActionResult Unauthorized()
        {
            return View("500");
        }
    }
}
