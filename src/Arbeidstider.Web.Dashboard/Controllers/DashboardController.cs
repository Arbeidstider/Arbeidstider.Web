using System;
using System.Web.Mvc;
using System.Web.Security;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.Services;
using Arbeidstider.Web.Framework.ViewModels.Account;
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

        public ActionResult Flushcache()
        {
            CheckAdminAccess();
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

        public ActionResult Register()
        {
            CheckAdminAccess();
            return View();
        }


        [HttpPost]
        public ActionResult Register(string password, string email, EmployeeDTO employee)
        {
            CheckAdminAccess();
            var model = new Register();

            if (_employeeService.CreateEmployee(employee))
            {
                var username = employee.GenerateUsername();
                var user = Membership.CreateUser(username, password, email);
                var member = Membership.GetUser(username);
                employee.UserID = (Guid)member.ProviderUserKey;
                model.Success = true;
            }

            return View(model);
        }
        
        public ActionResult UserProfile()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UserProfile(EmployeeDTO dto)
        {
            var updatedDto = _employeeService.UpdateEmployee(dto, HttpContext.User.Identity.Name);
            return Json(new { Result = true });
        }
    }
}