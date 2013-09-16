using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Arbeidstider.Business.Repository;
using TimesheetDTO = Arbeidstider.Services.DTO.TimesheetDTO;

namespace Arbeidstider.Services.Controllers
{
    public class TimesheetController : ApiController
    {
        public IEnumerable<TimesheetDTO> GetAllTimesheets(int employerID, DateTime startDate, DateTime endDate)
        {
            if (employerID == 0) throw new Exception("You have not specified a employerID.");
            if (endDate < startDate) throw new Exception("The end date must be a date happening after the start date.");
            //var startDate = new DateTime(2013, 9, 13);
            //var endDate = new DateTime(2013, 9, 15);
            //var employeeID = 1;
            var timesheets = TimesheetRepository.GetWeeklyTimesheets(employerID, startDate, endDate);
            return timesheets.Select(timesheet => new TimesheetDTO(timesheet.ShiftStart, timesheet.ShiftEnd)).ToList();
        }
    }
}
