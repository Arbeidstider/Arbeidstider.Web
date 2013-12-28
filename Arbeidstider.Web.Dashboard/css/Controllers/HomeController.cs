using System;
using System.Web.Mvc;

namespace Arbeidstider.Web.Dashboard.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
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
            return Json(new {Result = true});
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