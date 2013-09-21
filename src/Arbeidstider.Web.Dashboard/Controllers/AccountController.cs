using System;
using System.Web.Mvc;
using Arbeidstider.Business.Services;
using Arbeidstider.Web.Helpers;
using Arbeidstider.Web.Models;

namespace Arbeidstider.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserService _userservice;

        public AccountController()
        {
            _userservice = UserService.Instance;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (_userservice.VerifyUser(model.UserName, PasswordHelper.Hashpassword(model.Password)))
            {
                HttpContext.SetSession(Constants.Session.USERNAME, model.UserName);
                HttpContext.SetSession(Constants.Session.PASSWORD_HASH, PasswordHelper.Hashpassword(model.Password));
                HttpContext.SetCookie(Constants.Session.PASSWORD_HASH, PasswordHelper.Hashpassword(model.Password), DateTime.Now.AddDays(7));
                HttpContext.SetCookie(Constants.Session.USERNAME, model.UserName, DateTime.Now.AddDays(7));
                return RedirectToAction("Index", "Dashboard");
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        public ActionResult LogOff()
        {
            HttpContext.SetSession(Constants.Session.USERNAME, null);
            HttpContext.SetSession(Constants.Session.PASSWORD_HASH, null);
            HttpContext.SetCookie(Constants.Session.PASSWORD_HASH, null, null);
            HttpContext.SetCookie(Constants.Session.USERNAME, null, null);

            return RedirectToAction("Login", "Account");
        }
    }
}
