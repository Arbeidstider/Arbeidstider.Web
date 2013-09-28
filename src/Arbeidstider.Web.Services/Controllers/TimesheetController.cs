using System.Web.Mvc;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.Services;

namespace Arbeidstider.Web.Services.Controllers
{
    public class TimesheetController : BaseServiceController
    {
        private readonly TimesheetService _timesheetservice;

        public TimesheetController()
        {
            _timesheetservice = TimesheetService.Instance;
        }

        [HttpGet]
        public JsonResult GetAllTimesheets(TimesheetDTO dto)
        {

            var timesheets = _timesheetservice.GetAllWithinRange(dto);

            return Json(timesheets, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateTimesheet(TimesheetDTO dto)
        {
            var result = new JsonResult();
            /*
            if (!CurrentUser.HasAccessToEmployee(timesheet.EmployeeID))
            {
                result.Data = new {Result = false};
                return result;
            }
             */


            result.Data = new
            {
                Data = _timesheetservice.Create(dto),
                Result = true
            };

            return result;
        }
    }
}