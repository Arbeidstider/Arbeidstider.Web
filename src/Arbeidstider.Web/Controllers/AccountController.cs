using System.Web.Mvc;
using Arbeidstider.Business.Services;
using Arbeidstider.Web.Constants;
using Arbeidstider.Web.Helpers;
using Arbeidstider.Web.Models;

namespace Arbeidstider.Web.Controllers
{
    public class AccountController : Controller
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
                UserHelper.LoginEmployer(model);
                return Redirect(Urls.START_PAGE);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        public ActionResult LogOff()
        {
            UserHelper.LogOff();

            return Redirect(Urls.START_PAGE);
        }
    }
}
