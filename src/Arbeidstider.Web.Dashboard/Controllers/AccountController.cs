using System;
using System.Web.Mvc;
using Arbeidstider.Business.Domain;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Interfaces.Services;
using Arbeidstider.Web.Dashboard.Helpers;
using Arbeidstider.Web.Helpers;
using Arbeidstider.Web.Models;
using Arbeidstider.Web.Services.Models;
using Arbeidstider.Web.Services.Parameters;

namespace Arbeidstider.Web.Dashboard.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userservice;
        private readonly IRepository<Employer> _repository;

        public AccountController(IRepository<Employer> repository, IUserService userservice) : base(userservice)
        {
            _repository = repository;
            _userservice = userservice;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var userParameters = new UserParameters(new User()
            {
                Username = model.UserName,
                Passwordhash = PasswordHelper.Hashpassword(model.Password)
            }).Parameters;
            if (_userservice.VerifyUser(userParameters))
            {
                WebHelper.SetSession(Constants.Session.USERNAME, model.UserName);
                WebHelper.SetSession(Constants.Session.PASSWORD_HASH, PasswordHelper.Hashpassword(model.Password));
                WebHelper.SetCookie(Constants.Session.PASSWORD_HASH, PasswordHelper.Hashpassword(model.Password), DateTime.Now.AddDays(7));
                WebHelper.SetCookie(Constants.Session.USERNAME, model.UserName, DateTime.Now.AddDays(7));
                return RedirectToAction("Index", "Dashboard");
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        public ActionResult LogOff()
        {
            WebHelper.SetSession(Constants.Session.USERNAME, null);
            WebHelper.SetSession(Constants.Session.PASSWORD_HASH, null);
            WebHelper.SetCookie(Constants.Session.PASSWORD_HASH, null, null);
            WebHelper.SetCookie(Constants.Session.USERNAME, null, null);

            return RedirectToAction("Login", "Account");
        }
    }
}
