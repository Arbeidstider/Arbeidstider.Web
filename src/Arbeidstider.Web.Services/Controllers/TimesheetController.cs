using System;
using System.Linq;
using System.Web.Mvc;
using Arbeidstider.Business.Repository;
using Arbeidstider.Web.Services.DTO;
using Arbeidstider.Web.Services.Models;

namespace Arbeidstider.Web.Services.Controllers
{
    public class TimesheetController : BaseServiceController
    {
        [HttpGet]
        public JsonResult GetAllTimesheets(TimesheetDTO timesheet)
        {
            int employerID = timesheet.EmployerID;
            DateTime startDate = DateTime.Parse(timesheet.StartDate);
            DateTime endDate = DateTime.Parse(timesheet.EndDate);

            if (employerID == 0) throw new Exception("You have not specified a employerID.");
            if (endDate < startDate) throw new Exception("The end date must be a date happening after the start date.");

            var timesheets = TimesheetRepository.Instance.GetAllTimesheets(employerID, startDate, endDate);

            var timesheetDTOs = timesheets.Select(x => new TimesheetDTO(x.ShiftStart.ToString(), x.ShiftEnd.ToString(), employerID)).ToArray();
            return Json(timesheetDTOs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateTimesheet(TimesheetDTO timesheet)
        {
            var result = new JsonResult();
            if (!CurrentUser.HasAccessToEmployer(timesheet.EmployerID))
            {
                result.Data = new {Result = false};
                return result;
            }

            result.Data = new
            {
                Result = TimesheetRepository.Instance.CreateNewTimesheet(timesheet.EmployerID, timesheet.SelectedDay,
                    timesheet.ShiftStart, timesheet.ShiftEnd)
            };

            return result;
        }
    }
}
