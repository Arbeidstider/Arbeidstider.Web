﻿using System.Web.Mvc;
using Arbeidstider.Business.Interfaces.Services;
using Arbeidstider.Common.Parameters;
using Arbeidstider.Web.Framework.Helpers;
using Arbeidstider.Web.Framework.Services;

namespace Arbeidstider.Web.Dashboard.Filters
{
    public class AuthorizationAttribute : ActionFilterAttribute
    {
        private static readonly IUserService _userservice = UserService.Instance;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (IsLoggedIn()) return;
            filterContext.Result = new RedirectToRouteResult("login", null);
            base.OnActionExecuting(filterContext);
        }

        protected internal bool IsLoggedIn()
        {
            var sessionParameters = new UserParameters(
                WebHelper.GetSession(Web.Framework.Constants.Session.USERNAME),
                WebHelper.GetSession(Framework.Constants.Session.PASSWORD_HASH)
            ).Parameters;
            var cookieParameters = new UserParameters(
                WebHelper.GetCookie(Framework.Constants.Session.USERNAME), 
                WebHelper.GetCookie(Framework.Constants.Session.PASSWORD_HASH)
            ).Parameters;
            bool loggedIn = _userservice.VerifyUser(sessionParameters) || _userservice.VerifyUser(cookieParameters);
            return loggedIn;
        }
    }
}