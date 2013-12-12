using System;
using System.Web.Mvc;
using System.Web.Security;
using Arbeidstider.Web.Framework.Controllers;
using Arbeidstider.Web.Framework.ViewModels.Account;

namespace Arbeidstider.Web.Dashboard.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (EmployeeService.ValidateEmployee(model.UserName, model.Password))
            {
                var employee = EmployeeService.GetEmployee(model.UserName);
                if (employee != null)
                {
                    AuthenicateAndRedirect(model);
                }
            }

            return View(model);
        }

        private static void AuthenicateAndRedirect(LoginModel model)
        {
            FormsAuthentication.Authenticate(model.UserName, model.Password);
            if (model.RememberMe) FormsAuthentication.SetAuthCookie(model.UserName, true);
            FormsAuthentication.RedirectFromLoginPage(model.UserName, model.RememberMe);
        }

        public ActionResult LogOff()
        {
            try
            {
                FormsAuthentication.SignOut();
                return Redirect("/");
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Failed to logoff user: {0}", CurrentUser));
            }
            return RedirectToAction("Index", "Dashboard");
        }
    }
}