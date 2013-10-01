using System;
using System.Web.Mvc;
using System.Web.Security;
using Arbeidstider.Web.Framework;
using Arbeidstider.Web.Framework.Services;
using Arbeidstider.Web.Framework.ViewModels.Account;
using log4net;

namespace Arbeidstider.Web.Dashboard.Controllers
{
    public class AccountController : BaseController
    {
        private readonly EmployeeService _employeeService;
        private readonly ILog Logger;
        public AccountController()
        {
            _employeeService = EmployeeService.Instance;
            Logger = IoC.Resolve<ILog>();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (Membership.ValidateUser(model.UserName, model.Password))
            {
                var employee = _employeeService.GetEmployee(model.UserName);
                if (employee != null)
                {
                    return AuthenicateAndRedirect(model);
                }
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        private ActionResult AuthenicateAndRedirect(LoginModel model)
        {
            FormsAuthentication.Authenticate(model.UserName, model.Password);
            if (model.RememberMe) FormsAuthentication.SetAuthCookie(model.UserName, true);
            FormsAuthentication.RedirectFromLoginPage(model.UserName, model.RememberMe);
            return RedirectToAction("Index", "Dashboard");
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