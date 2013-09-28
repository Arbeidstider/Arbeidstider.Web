using System;
using System.Web.Mvc;
using Arbeidstider.Web.Framework.Helpers;

namespace Arbeidstider.Web.Dashboard.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
        }

        protected internal int CurrentEmployeeID
        {
            get
            {
                if (WebHelper.GetSession(Framework.Constants.Session.EMPLOYEE_ID) != null)
                    return int.Parse(WebHelper.GetSession(Framework.Constants.Session.EMPLOYEE_ID));

                if (WebHelper.GetCookie(Framework.Constants.Session.EMPLOYEE_ID) != null)
                    return int.Parse(WebHelper.GetCookie(Framework.Constants.Session.EMPLOYEE_ID));

                return 0;
            }
            set
            {
                if (value == 0)
                {
                    WebHelper.RemoveSession(Framework.Constants.Session.EMPLOYEE_ID);
                    WebHelper.RemoveCookie(Framework.Constants.Session.EMPLOYEE_ID);
                }
                else
                {
                    WebHelper.SetSession(Framework.Constants.Session.EMPLOYEE_ID, value);
                    WebHelper.SetCookie(Framework.Constants.Session.EMPLOYEE_ID, value, DateTime.Now.AddDays(7));
                }
            }
        }
    }
}
