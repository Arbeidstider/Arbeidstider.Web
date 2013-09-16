using System.Web;
using System.Web.Mvc;
using Arbeidstider.Business.Domain;
using Arbeidstider.Business.Services;
using Arbeidstider.Web.Constants;

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
            bool remembeMe = HttpContext.Current.Response.Cookies[Session.USERNAME] != null &&
                             HttpContext.Current.Response.Cookies[Session.PASSWORD_HASH] != null &&
                             UserService.VerifyUser(HttpContext.Current.Response.Cookies[Session.USERNAME].ToString(), HttpContext.Current.Response.Cookies[Session.PASSWORD_HASH].ToString());

            if (remembeMe) return true;

            bool loggedIn = HttpContext.Current.Session["Username"] != null && HttpContext.Current.Session["Passwordhash"] != null && UserService.VerifyUser(HttpContext.Current.Session["Username"].ToString(), HttpContext.Current.Session["Passwordhash"].ToString());
            return loggedIn;
        }
    }
}