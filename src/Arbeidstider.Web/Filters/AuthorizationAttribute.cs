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
            if (UserIsLoggedIn()) return;
            filterContext.Result = new RedirectResult("/login");
        }

        private static string GetSession(string key)
        {
            if (HttpContext.Current.Session[key] == null) return null;
            return (string) HttpContext.Current.Session[key];
        }

        private static string GetCookie(string key)
        {
            if (HttpContext.Current.Response.Cookies[key] == null) return null;
            return (string) HttpContext.Current.Response.Cookies[key].Value;
        }

        public static bool UserIsLoggedIn()
        {
            return (UserService.Instance.VerifyUser(GetCookie(Session.USERNAME), GetCookie(Session.PASSWORD_HASH)) || 
                UserService.Instance.VerifyUser(GetSession(Session.USERNAME), GetSession(Session.PASSWORD_HASH)));
        }
    }
}