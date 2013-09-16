using System.Web;
using System.Web.Mvc;
using Arbeidstider.Business.Domain;
using Arbeidstider.Business.Services;

namespace Arbeidstider.Web.Filters
{
    public sealed class AuthorizationAttribute : ActionFilterAttribute
    {
        private EmployerGroup _employerGroup;
        public AuthorizationAttribute() {}
        public AuthorizationAttribute(EmployerGroup AccessLevelNeeded)
        {
            _employerGroup = AccessLevelNeeded;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!UserIsLoggedIn())
            {
                filterContext.Result = new RedirectResult("/login");
                return;
            }
        }

        public static bool UserIsLoggedIn()
        {
            bool loggedIn = HttpContext.Current.Session["Username"] != null && HttpContext.Current.Session["Passwordhash"] != null && UserService.VerifyUser(HttpContext.Current.Session["Username"].ToString(), HttpContext.Current.Session["Passwordhash"].ToString());
            return loggedIn;
        }
    }
}