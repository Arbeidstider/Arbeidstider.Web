using System;
using System.Web.Mvc;
using Arbeidstider.Business.Services;
using Arbeidstider.Web.Constants;
using Arbeidstider.Web.Helpers;
using Arbeidstider.Web.Models;
using Microsoft.Ajax.Utilities;

namespace Arbeidstider.Web.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Login()
        {
            //ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (UserService.Instance.VerifyUser(model.UserName, PasswordHelper.Hashpassword(model.Password)))
            {
                HttpContext.SetSession(Constants.Session.USERNAME, model.UserName);
                HttpContext.SetSession(Constants.Session.PASSWORD_HASH, PasswordHelper.Hashpassword(model.Password));
                HttpContext.SetCookie(Constants.Session.PASSWORD_HASH, PasswordHelper.Hashpassword(model.Password), DateTime.Now.AddDays(7));
                HttpContext.SetCookie(Constants.Session.USERNAME, model.UserName, DateTime.Now.AddDays(7));
                return Redirect(Urls.DASH_BOARD);
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

            return Redirect(Urls.START_PAGE);
        }
    }
}
