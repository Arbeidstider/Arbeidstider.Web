using System;
using System.Web.Mvc;
using Arbeidstider.Web.Framework.ViewModels.Error;

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
            var model = new ErrorModel();
            model.Exception = exception;
            model.StatusCode = statusCode;
            return View("500", model);
        }

        public ActionResult Unauthorized()
        {
            return View("500");
        }
    }
}
