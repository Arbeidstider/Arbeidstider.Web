using System;
using System.Collections.Generic;
using Arbeidstider.Web.Framework.DTO;

namespace Arbeidstider.Web.Framework.Models
{
    [Serializable]
    public class CalendarDay
    {
        public CalendarDay(DayOfWeek day, IEnumerable<ShiftDTO> shifts)
        {
            DayOfWeek = day;
            Shifts = shifts;
        }

        public DayOfWeek DayOfWeek { get; set; }
        public DateTime? Date { get; set; }
        public int WorkplaceId { get; set; }
        // filter on employee
        public int? EmployeeId { get; set; }
        public IEnumerable<ShiftDTO> Shifts { get; set; }
    }
}