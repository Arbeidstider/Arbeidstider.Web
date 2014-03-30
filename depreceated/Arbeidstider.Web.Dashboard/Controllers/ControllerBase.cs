using System;
using System.Web.Mvc;

namespace Arbeidstider.Web.Dashboard.Controllers
{
    public class ControllerBase : Controller
    {
        public int CurrentEmployeeId
        {
            get
            {
                var cookies = HttpContext.Response.Cookies;
                foreach (var cookie in cookies)
                {
                    // find cookie and get employeeId
                    Console.WriteLine(cookie);
                }
                return 1;
            }
        }
    }
}