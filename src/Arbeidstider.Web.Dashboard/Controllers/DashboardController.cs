using System;
using System.Web.Mvc;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.Services;
using Arbeidstider.Web.Framework.ViewModels.Dashboard;

namespace Arbeidstider.Web.Dashboard.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly TimesheetService _timesheetService;
        private readonly EmployeeService _employeeService;

        public DashboardController() : base()
        {
            _employeeService = EmployeeService.Instance;
            _timesheetService = TimesheetService.Instance;
        }

        public ActionResult Index()
        {
            var model = new Index();
            //model.CurrentWeeklyWorkHours = _timesheetService.GetWeeklyTimesheet(CurrentEmployeeID, new DateTime(2013, 9, 23));
            return View();
        }

        public ActionResult AddressBook()
        {
            return View();
        }

        public ActionResult ConfirmShifts()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public JsonResult Register(EmployeeDTO dto)
        {
            _employeeService.UpdateEmployee(dto, dto.UserID);
            return Json(true);
        }
        

        public ActionResult UserProfile()
        {
            return View();
        }
    }
}