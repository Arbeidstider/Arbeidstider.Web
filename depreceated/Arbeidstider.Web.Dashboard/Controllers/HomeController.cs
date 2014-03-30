using System;
using System.Web.Mvc;
using Arbeidstider.Web.Dashboard.Models;
using Arbeidstider.Web.Framework.Services;

namespace Arbeidstider.Web.Dashboard.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            var model = new Index();
            Console.WriteLine(HttpContext.Response.Cookies);
            foreach (var item in HttpContext.Response.Cookies)
            {
                Console.WriteLine(item);
            }
            model.TimesheetCalendar = TimesheetService.Instance.GetCurrentTimesheetWeek(employeeId: CurrentEmployeeId);
            return View(model);
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult AddressBook()
        {
            /*
            var model = new AddressBook();
            model.Colleagues = EmployeeService.GetAllEmployees(CurrentEmployee.WorkplaceID);
             */
            return View();
        }

        public ActionResult ConfirmShifts()
        {
            return View();
        }

        public ActionResult Register()
        {
            //ValidateManagerAccess();
            return View();
        }

        [HttpGet]
        public JsonResult ValidateEmployee()
        {
            return Json(new { Result = true });
        }

        public ActionResult UserProfile()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UserProfile(Guid userID)
        {
            return Json(new { Result = true });
        }
    }
}