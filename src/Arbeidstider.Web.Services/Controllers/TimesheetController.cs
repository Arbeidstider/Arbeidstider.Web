using System;
using System.Web.Mvc;
using Arbeidstider.Web.Framework.Controllers;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.Services;

namespace Arbeidstider.Web.Services.Controllers
{
    public class TimesheetController : BaseController
    {
        private readonly TimesheetService _timesheetService;
        public TimesheetController()
        {
            _timesheetService = TimesheetService.Instance;
        }

        [HttpGet]
        public JsonResult GetAllTimesheets(DateTime startDate, DateTime endDate, Guid userID)
        {
            var timesheets = _timesheetService.GetAllWithinRange(startDate, endDate, userID);

            return Json(timesheets, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateTimesheet(TimesheetDTO dto)
        {
            var result = new JsonResult();

            if (!(CurrentEmployee.IsManager() && dto.WorkplaceID == CurrentWorkplaceID) 
                || !CurrentEmployee.IsAdmin())
            {
                return Json(new {Result = false});
            }


            result.Data = new
            {
                Data = _timesheetService.Create(dto),
                Result = true
            };

            return result;
        }
    }
}