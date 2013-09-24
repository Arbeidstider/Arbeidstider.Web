using System;
using System.Web.Mvc;
using Arbeidstider.Business.Interfaces.Services;
using Arbeidstider.Common.Parameters;
using Arbeidstider.Web.Framework.Helpers;
using Arbeidstider.Web.Framework.Services;
using Arbeidstider.Web.Framework.ViewModels.Account;

namespace Arbeidstider.Web.Dashboard.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userservice;

        public AccountController() : base()
        {
            _userservice = UserService.Instance;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var userParameters = new UserParameters(
                model.UserName,
                PasswordHelper.Hashpassword(model.Password)
            ).Parameters;
            if (_userservice.VerifyUser(userParameters))
            {
                WebHelper.SetSession(Framework.Constants.Session.USERNAME, model.UserName);
                WebHelper.SetSession(Framework.Constants.Session.PASSWORD_HASH, PasswordHelper.Hashpassword(model.Password));
                WebHelper.SetCookie(Framework.Constants.Session.PASSWORD_HASH, PasswordHelper.Hashpassword(model.Password), DateTime.Now.AddDays(7));
                WebHelper.SetCookie(Framework.Constants.Session.USERNAME, model.UserName, DateTime.Now.AddDays(7));
                return RedirectToAction("Index", "Dashboard");
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        public ActionResult LogOff()
        {
            WebHelper.SetSession(Framework.Constants.Session.USERNAME, null);
            WebHelper.SetSession(Framework.Constants.Session.PASSWORD_HASH, null);
            WebHelper.SetCookie(Framework.Constants.Session.PASSWORD_HASH, null, null);
            WebHelper.SetCookie(Framework.Constants.Session.USERNAME, null, null);

            return RedirectToAction("Login", "Account");
        }
    }
}
