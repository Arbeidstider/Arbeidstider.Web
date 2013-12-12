using System;
using System.Web.Mvc;
using Arbeidstider.Web.Dashboard.Filters;
using Arbeidstider.Web.Framework.Controllers;
using Arbeidstider.Web.Framework.ViewModels.Dashboard;

namespace Arbeidstider.Web.Dashboard.Controllers
{
    public class DashboardController : BaseController
    {
        public DashboardController() : base()
        {
        }

        [AdminAccess]
        public ActionResult Flushcache()
        {
            return Index();
        }

        public ActionResult Index()
        {
            var model = new Index();
            model.Shifts = TimesheetService.GetWeeklyShifts(CurrentUserID, DateTime.Now);
            return View();
        }

        public ActionResult AddressBook()
        {
            var model = new AddressBook();
            model.Colleagues = EmployeeService.GetAllEmployees(CurrentEmployee.WorkplaceID);
            return View(model);
        }

        public ActionResult ConfirmShifts()
        {
            return View();
        }

        public ActionResult Register()
        {
            ValidateManagerAccess();
            return View();
        }

        private void ValidateManagerAccess()
        {
            if (!CurrentEmployee.IsAdmin() || !CurrentEmployee.IsManager())
                RedirectToAction("Unauthorized", "Error");
        }

        [HttpGet]
        [AdminAccess]
        public JsonResult ValidateEmployee()
        {
            return Json(new {Result = true});
        }

        [HttpPost]
        public ActionResult Register(NewEmployee employee)
        {
            ValidateManagerAccess();
            employee.WorkplaceID = CurrentWorkplaceID;
            employee = EmployeeService.Register(employee);
            return View(employee);
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