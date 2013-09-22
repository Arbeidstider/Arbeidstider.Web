using System.Web;
using System.Web.Mvc;
using Arbeidstider.Business.Interfaces;
using Arbeidstider.Business.Interfaces.Services;
using Arbeidstider.Business.Services;
using Arbeidstider.Web.Dashboard.Helpers;
using Arbeidstider.Web.Services.Models;
using Arbeidstider.Web.Services.Parameters;
using Autofac;

namespace Arbeidstider.Web.Dashboard.Filters
{
    public class AuthorizationAttribute : ActionFilterAttribute
    {
        private static readonly IUserService _userservice = DependencyResolver.Current.GetService<IUserService>();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (IsLoggedIn()) return;
            filterContext.Result = new RedirectToRouteResult("login", null);
            base.OnActionExecuting(filterContext);
        }

        protected internal bool IsLoggedIn()
        {
            var sessionParameters = new UserParameters(new User()
            {
                Username = WebHelper.GetSession(Constants.Session.USERNAME),
                Passwordhash = WebHelper.GetSession(Constants.Session.PASSWORD_HASH)
            }).Parameters;
            var cookieParameters = new UserParameters(new User()
            {
                Username = WebHelper.GetCookie(Constants.Session.USERNAME), 
                Passwordhash = WebHelper.GetCookie(Constants.Session.PASSWORD_HASH)
            }).Parameters;
            bool loggedIn = _userservice.VerifyUser(sessionParameters) || _userservice.VerifyUser(cookieParameters);
            return loggedIn;
        }
    }
}