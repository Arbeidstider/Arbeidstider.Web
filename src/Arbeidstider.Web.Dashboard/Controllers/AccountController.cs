using System;
using System.Web.Mvc;
using Arbeidstider.Business.Interfaces.Services;
using Arbeidstider.Business.Services;
using Arbeidstider.Web.Helpers;
using Arbeidstider.Web.Models;
using Arbeidstider.Web.Services.Models;
using Arbeidstider.Web.Services.Parameters;

namespace Arbeidstider.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userservice;

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
            var userParameters = new UserParameters(new User()
            {
                Username = model.UserName,
                Passwordhash = PasswordHelper.Hashpassword(model.Password)
            }).Parameters;
            if (_userservice.VerifyUser(userParameters))
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
