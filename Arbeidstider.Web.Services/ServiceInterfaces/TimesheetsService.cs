using System;
using System.Collections.Generic;
using System.Linq;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.Services;
using Arbeidstider.Web.Services.ServiceModels;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    //[CustomAuthenticate("EmployeeAuth")]
    public class TimesheetsService : ServiceInterfaceBase
    {
        public object Options(Timesheets request)
        {
            return Get(request);
        }

        public object Get(Timesheets request)
        {
            var response = new TimesheetsResponse();
            try
            {
                if (request.WeeklyView != null && (bool)request.WeeklyView)
                {
                    response.WeeklyTimesheetCalendar = TimesheetService.Instance.GetCurrentTimesheetWeek(request.EmployeeId, request.WorkplaceId);
                }
                response.Timesheets = (IEnumerable<TimesheetDTO>) GetTimesheetsWithinRange(request);
            }
            catch
            {
                // TODO: Improve error handling
                response.Timesheets = new List<TimesheetDTO>();
            }
            return response;
        }

        private static IEnumerable<TimesheetDTO> GetTimesheetsWithinRange(Timesheets request)
        {
            DateTime endDate, startDate;
            if (DateTime.TryParse(request.StartDate, out startDate) && DateTime.TryParse(request.EndDate, out endDate))
            {
                return TimesheetService.Instance.GetAllWithinRange(
                    startDate,
                    endDate,
                    request.EmployeeId,
                    request.WorkplaceId);
            }
            return new List<TimesheetDTO>();
        }
    }
}