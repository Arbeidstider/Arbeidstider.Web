using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Arbeidstider.Business.Services;
using Arbeidstider.Web.App_Start;
using Arbeidstider.Web.Helpers;
using Arbeidstider.Web.Services.Models;
using Arbeidstider.Web.Services.Parameters;

namespace Arbeidstider.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (Context.Request.Url.ToString().ToLower().Contains("assets")) return;
            if (IsLoggedIn()) return;
            if (Context.Request.Url.ToString().ToLower().EndsWith("login")) return;
            Context.Response.RedirectToRoute("login");
        }

        private bool IsLoggedIn()
        {
            var sessionParameters = new UserParameters(new User()
            {
                Username = Context.GetSession(Constants.Session.USERNAME),
                Passwordhash = Context.GetSession(Constants.Session.PASSWORD_HASH)
            }).Parameters;
            var cookieParameters = new UserParameters(new User()
            {
                Username = Context.GetCookie(Constants.Session.USERNAME), 
                Passwordhash = Context.GetCookie(Constants.Session.PASSWORD_HASH)
            }).Parameters;
            bool loggedIn = UserService.Instance.VerifyUser(sessionParameters) || UserService.Instance.VerifyUser(cookieParameters);
            return loggedIn;
        }
    }
}