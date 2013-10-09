using System.Web.Mvc;
using Arbeidstider.Web.Framework.Controllers;
using Arbeidstider.Web.Framework.DTO;

namespace Arbeidstider.Web.Services.Controllers
{
    public class TimesheetController : BaseController
    {
        public TimesheetController()
        {
        }

        [HttpGet]
        public JsonResult GetAllTimesheets(TimesheetDTO dto)
        {
            var timesheets = TimesheetService.GetAllWithinRange(dto);

            return Json(timesheets, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateTimesheet(TimesheetDTO dto)
        {
            var result = new JsonResult();

            if (!CurrentEmployee.IsManager() || CurrentWorkplaceID != dto.WorkplaceID)
            {
                return Json(new {Result = false});
            }


            result.Data = new
            {
                Data = TimesheetService.Create(dto),
                Result = true
            };

            return result;
        }
    }
}