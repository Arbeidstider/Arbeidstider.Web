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
                    return GetWeeklyTimesheets(request);
                }
                return GetTimesheetsWithinRange(request);
            }
            catch
            {
                // TODO: Improve error handling
                response.Timesheets = new List<TimesheetDTO>();
            }
            return response;
        }

        private static object GetTimesheetsWithinRange(Timesheets request)
        {
            var response = new TimesheetsResponse();
            DateTime endDate, startDate;
            if (DateTime.TryParse(request.StartDate, out startDate) && DateTime.TryParse(request.EndDate, out endDate))
            {
                response.Timesheets = TimesheetService.Instance.GetAllWithinRange(
                    startDate,
                    endDate,
                    request.EmployeeId,
                    request.WorkplaceId);
            }
            return response;
        }

        private object GetWeeklyTimesheets(Timesheets request)
        {
            var response = new TimesheetsResponse();
            var weeklyTimesheets = new List<DailyTimesheet>();
            var monday = GetMondayDate(request);

            var dtos = TimesheetService.Instance.GetAllWithinRange(
                monday,
                monday.AddDays(6),
                request.EmployeeId,
                request.WorkplaceId);

            var results = dtos as TimesheetDTO[] ?? dtos.ToArray();

            // Monday thru Saturday, 1....6, sunday == 7
            for (int i = 1; i < 8; i++)
            {
                IEnumerable<TimesheetDTO> dailyTimesheets;
                // sunday, last day of week, represented as 0 in .net, gets added last to our array;
                if (i == 7)
                    dailyTimesheets = GetTimesheetsByDay(results, 0);
                else
                    dailyTimesheets = GetTimesheetsByDay(results, i);

                if (!dailyTimesheets.Any())
                    dailyTimesheets = new List<TimesheetDTO>();

                weeklyTimesheets.Add(new DailyTimesheet() { DayOfWeek = i, Timesheets = dailyTimesheets });
            }
            response.WeeklyTimesheets = weeklyTimesheets.OrderBy(x => x.DayOfWeek);
            return response;
        }

        private static DateTime GetMondayDate(Timesheets request)
        {
            if (string.IsNullOrEmpty(request.StartDate))
            {
                if (DateTime.Now.DayOfWeek != DayOfWeek.Monday) return FindMondayDate(DateTime.Now);
                return DateTime.Now;
            }

            DateTime md;
            if (DateTime.TryParse(request.StartDate, out md) && md.DayOfWeek == DayOfWeek.Monday)
                return md;

            return DateTime.MinValue;
        }

        private static IEnumerable<TimesheetDTO> GetTimesheetsByDay(IEnumerable<TimesheetDTO> results, int i)
        {
            return (from x in results
                    where !string.IsNullOrEmpty(x.ShiftDate)
                       && (((int)(DateTime.Parse(x.ShiftDate).DayOfWeek)) == i)
                    select x).ToArray();

        }

        private static DateTime FindMondayDate(DateTime date)
        {
            while (date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.AddDays(-1);
                if (date.DayOfWeek == DayOfWeek.Monday) break;
            }

            return date;
        }
    }
}