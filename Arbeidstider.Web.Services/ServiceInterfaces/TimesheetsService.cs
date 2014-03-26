using System;
using System.Collections.Generic;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.Services;
using Arbeidstider.Web.Services.Attributes;
using Arbeidstider.Web.Services.Exceptions;
using Arbeidstider.Web.Services.ServiceModels;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    [CustomAuthenticate("Employee")]
    public class TimesheetsService : ServiceBase
    {
        public object Delete(DeleteTimesheet request)
        {
            return true;
        }

        public object Put(UpdateTimesheets request)
        {
            DateTime date;
            TimeSpan ss;
            TimeSpan se;
            DateTime.TryParse(request.Date, out date);
            TimeSpan.TryParse(request.ShiftStart, out ss);
            TimeSpan.TryParse(request.ShiftEnd, out se);
            var result = TimesheetService.Instance.UpdateTimesheet(
                request.Id,
                request.EmployeeId,
                date,
                ss,
                se
            );

            return result;
        }

        public object Patch(UpdateTimesheets request)
        {
            return true;
        }

        public object Post(CreateTimesheet request)
        {
            if (request.EmployeeId == 0)
                throw new TimesheetServiceException("You need to specify EmployeeId for TimesheetService.Create");
            if (request.SelectedDay == string.Empty)
                throw new TimesheetServiceException("You need to specify SelectedDay for TimesheetService.Create");
            if (request.ShiftStart == string.Empty)
                throw new TimesheetServiceException("You need to specify ShiftStart for TimesheetService.Create");
            if (request.ShiftEnd == null)
                throw new TimesheetServiceException("You need to specify ShiftEnd for TimesheetService.Create");

            DateTime selectedDay;
            TimeSpan shiftStart;
            TimeSpan shiftEnd;

            DateTime.TryParse(request.SelectedDay, out selectedDay);
            TimeSpan.TryParse(request.ShiftStart, out shiftStart);
            TimeSpan.TryParse(request.ShiftEnd, out shiftEnd);

            int id = Arbeidstider.Web.Framework.Services.TimesheetService.Instance.CreateTimesheet(
                request.EmployeeId,
                selectedDay,
                shiftStart,
                shiftEnd);
            if (id != 0)
            {
                return Framework.Services.TimesheetService.Instance.GetTimesheet(id);
            }

            return null;
        }

        public object Get(FindTimesheets request)
        {
            List<TimesheetDTO> results = new List<TimesheetDTO>();
            try
            {
                //if (request.WeeklyView != null && (bool)request.WeeklyView)
                //{
                //    // refactor back to use ienumerable
                //    response.WeeklyTimesheetCalendar = TimesheetService.Instance.GetCurrentTimesheetWeek(request.EmployeeId, request.WorkplaceId);
                //}
                results.AddRange((IEnumerable<TimesheetDTO>)GetTimesheetsWithinRange(request.StartDate, request.EndDate, request.EmployeeId, request.WorkplaceId));
            }
            catch
            {
                // TODO: Improve error handling
            }
            return results;
        }

        private static IEnumerable<TimesheetDTO> GetTimesheetsWithinRange(string startDate, string endDate, int? employeeId, int? workplaceId)
        {
            DateTime ed, sd;
            if (DateTime.TryParse(startDate, out sd) && DateTime.TryParse(endDate, out ed))
            {
                return TimesheetService.Instance.GetAllWithinRange(
                    sd,
                    ed,
                    employeeId,
                    workplaceId);
            }
            return new List<TimesheetDTO>();
        }
    }
}