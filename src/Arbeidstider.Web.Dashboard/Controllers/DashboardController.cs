using System;
using System.Web.Mvc;
using System.Web.Security;
using Arbeidstider.Web.Dashboard.Filters;
using Arbeidstider.Web.Framework.Controllers;
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

        [AdminAccess]
        public ActionResult Flushcache()
        {
            return Index();
        }

        public ActionResult Index()
        {
            var model = new Index();
            model.Shifts = _timesheetService.GetWeeklyTimesheet(CurrentUserID, DateTime.Now);
            return View();
        }

        public ActionResult AddressBook()
        {
            var model = new AddressBook();
            model.Colleagues = _employeeService.GetAllEmployees(CurrentEmployee.WorkplaceID);
            return View(model);
        }

        public ActionResult ConfirmShifts()
        {
            return View();
        }

        [AdminAccess]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        [AdminAccess]
        public JsonResult ValidateEmployee(EmployeeDTO employee)
        {
            return Json(new {Result = true});
        }

        [AdminAccess]
        [HttpPost]
        public ActionResult Register(NewEmployee employee)
        {
            var username = employee.GenerateUsername();
            var password = employee.GeneratePassword();
            Membership.CreateUser(username, password);
            var member = Membership.GetUser(username);
            if (_employeeService.CreateEmployee(
                EmployeeDTO.Create(
                username: username, 
                userID: (Guid)member.ProviderUserKey,
                lastname: employee.Lastname,
                firstname: employee.Firstname,
                mobile: employee.Mobile,
                birthdate: employee.BirthDate
                )))
            {
                employee.Success = true;
            }

            return View(employee);
        }
        
        public ActionResult UserProfile()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UserProfile(EmployeeDTO dto)
        {
            return Json(new { Result = true });
        }
    }
}