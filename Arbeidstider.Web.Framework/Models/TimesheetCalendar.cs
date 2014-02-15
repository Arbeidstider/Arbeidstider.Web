using System;
using System.Collections.Generic;
using System.Linq;
using Arbeidstider.Interfaces;
using Arbeidstider.Web.Framework.DTO;

namespace Arbeidstider.Web.Framework.Models
{
    [Serializable]
    public class TimesheetCalendar
    {
        private const int SUNDAY = 7;
        private const int WEEK_LENGTH = 7;
        public TimesheetCalendar(IEnumerable<ITimesheet> timesheets)
        {
            InitWeek(timesheets);
        }

        private void InitWeek(IEnumerable<ITimesheet> timesheets)
        {
            // Monday thru Saturday, 1....6, sunday == 7
            // TODO: Return proper structure in sp
            for (int i = (int)DayOfWeek.Monday; i < WEEK_LENGTH + 1; i++)
            {
                DayOfWeek day;
                // convert to .NET standard
                if (i == SUNDAY) day = DayOfWeek.Sunday;
                else day = (DayOfWeek)i;

                var shifts = GetShiftsByDay(day, timesheets);
                PopulateDay(day, shifts);
            }
        }

        private void PopulateDay(DayOfWeek day, IEnumerable<ShiftDTO> shifts)
        {
            var calendarDay = new CalendarDay(day, shifts);
            switch ((DayOfWeek)day)
            {
                case DayOfWeek.Monday:
                    Monday = calendarDay;
                    break;
                case DayOfWeek.Tuesday:
                    Tuesday = calendarDay;
                    break;
                case DayOfWeek.Wednesday:
                    Wednesday = calendarDay;
                    break;
                case DayOfWeek.Thursday:
                    Thursday = calendarDay;
                    break;
                case DayOfWeek.Friday:
                    Friday = calendarDay;
                    break;
                case DayOfWeek.Saturday:
                    Saturday = calendarDay;
                    break;
                case DayOfWeek.Sunday:
                    Sunday = calendarDay;
                    break;
            }
        }

        private static IEnumerable<ShiftDTO> GetShiftsByDay(DayOfWeek dayOfWeek, IEnumerable<ITimesheet> results)
        {
            return (from x in results
                    where x.ShiftDate.DayOfWeek == dayOfWeek
                    select new ShiftDTO(x)).ToList();
        }

        public CalendarDay Monday { get; set; }
        public CalendarDay Tuesday { get; set; }
        public CalendarDay Wednesday { get; set; }
        public CalendarDay Thursday { get; set; }
        public CalendarDay Friday { get; set; }
        public CalendarDay Saturday { get; set; }
        public CalendarDay Sunday { get; set; }
    }
}