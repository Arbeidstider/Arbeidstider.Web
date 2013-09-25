using System;
using System.Web;
using System.Web.Mvc;
using Arbeidstider.Web.Framework.Helpers;
using Arbeidstider.Web.Framework.ViewModels.Account;

namespace Arbeidstider.Web.Dashboard.Controllers
{
    public class BaseController : Controller
    {
        private readonly HttpContextBase _context;

        public BaseController()
        {
            _context = HttpContext;
        }

        protected internal void SetCurrentUser(EmployeeUser user)
        {
            WebHelper.SetSession(Framework.Constants.Session.USERNAME, user.Username);
            WebHelper.SetSession(Framework.Constants.Session.PASSWORD_HASH, user.Passwordhash);
            WebHelper.SetSession(Framework.Constants.Session.EMPLOYEE_ID, user.EmployeeID);
            WebHelper.SetCookie(Framework.Constants.Session.PASSWORD_HASH, user.Passwordhash, DateTime.Now.AddDays(7));
            WebHelper.SetCookie(Framework.Constants.Session.USERNAME, user.Username, DateTime.Now.AddDays(7));
            WebHelper.SetCookie(Framework.Constants.Session.EMPLOYEE_ID, user.EmployeeID, DateTime.Now.AddDays(7));
        }

        protected internal int GetCurrentEmployeeID
        {
            get
            {
                if (WebHelper.GetSession(Framework.Constants.Session.EMPLOYEE_ID) != null)
                    return int.Parse(WebHelper.GetSession(Framework.Constants.Session.EMPLOYEE_ID));

                if (WebHelper.GetCookie(Framework.Constants.Session.EMPLOYEE_ID) != null)
                    return int.Parse(WebHelper.GetCookie(Framework.Constants.Session.EMPLOYEE_ID));

                return 0;
            }
        }
    }
}
