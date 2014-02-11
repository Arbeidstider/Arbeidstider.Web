using System.Collections.Generic;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.Models;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    public class Timesheets : IReturn<TimesheetsResponse>
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? EmployeeId { get; set; }
        public int? WorkplaceId { get; set; }
        public bool? WeeklyView { get; set; }
    }

    public class TimesheetsResponse
    {
        public IEnumerable<TimesheetDTO> Timesheets { get; set; }
        // Refactor back to use array instead of days
        public IEnumerable<CalendarDay> WeeklyTimesheetCalendar { get; set; }
        public ResponseStatus ResponseStatus { get; set; } //Where Exceptions get auto-serialized
    }
}