using System;
using System.Web.Mvc;
using System.Web.Security;
using Arbeidstider.Web.Framework.Services;
using Arbeidstider.Web.Framework.ViewModels.Account;

namespace Arbeidstider.Web.Dashboard.Controllers
{
    public class AccountController : BaseController
    {
        private readonly EmployeeService _employeeService;
        public AccountController()
        {
            _employeeService = EmployeeService.Instance;
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
                    CurrentEmployeeID = employee.EmployeeID;
                    FormsAuthentication.Authenticate(model.UserName, model.Password);
                    if (model.RememberMe) FormsAuthentication.SetAuthCookie(model.UserName, true);
                    FormsAuthentication.RedirectFromLoginPage(model.UserName, model.RememberMe);
                    return RedirectToAction("Index", "Dashboard");
                }
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        [HttpGet]
        public JsonResult LogOff()
        {
            try
            {
                CurrentEmployeeID = 0;
                FormsAuthentication.SignOut();
            }
            catch (Exception ex)
            {
                return Json(new {Result = false});
            }
            return Json(new {Result = true});
        }
    }
}