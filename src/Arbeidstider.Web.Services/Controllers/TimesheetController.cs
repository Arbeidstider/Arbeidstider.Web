using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Filters;
using Arbeidstider.Business.Repository;
using Arbeidstider.Web.Services.DTO;

namespace Arbeidstider.Web.Services.Controllers
{
    public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response != null)
                actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            base.OnActionExecuted(actionExecutedContext);
        }
    }

    [AllowCrossSiteJson]
    public class TimesheetController : ApiController
    {
        [HttpGet]
        [HttpOptions]
        public IEnumerable<TimesheetDTO> GetAllTimesheets(int employerID, DateTime startDate, DateTime endDate)
        {
            if (employerID == 0) throw new Exception("You have not specified a employerID.");
            if (endDate < startDate) throw new Exception("The end date must be a date happening after the start date.");

            var timesheets = TimesheetRepository.Instance.GetAllTimesheets(employerID, startDate, endDate);
            return timesheets.Select(timesheet => new TimesheetDTO(timesheet.ShiftStart, timesheet.ShiftEnd, 1)).ToList();
        }
    }
}
