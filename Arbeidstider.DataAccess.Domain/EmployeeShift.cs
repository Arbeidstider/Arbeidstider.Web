using System;
using Arbeidstider.Interfaces;

namespace Arbeidstider.DataAccess.Domain
{
    public class EmployeeShift : IEmployeeShift
    {
        public EmployeeShift(ITimesheet timesheet)
        {
            if (timesheet != null)
            {
                ShiftDate = timesheet.ShiftDate;
                EmployeeId = timesheet.EmployeeId;
                ShiftEnd = timesheet.ShiftEnd;
                ShiftStart = timesheet.ShiftStart;
            }
        }

        public DateTime? ShiftDate { get; set; }
        public int? EmployeeId { get; set; }

        private int _dayOfWeek;
        public int DayOfWeek
        {
            get 
            {
                if (ShiftDate != null) return (int) ShiftDate.Value.DayOfWeek;
                return _dayOfWeek;
            }
            set { _dayOfWeek = value; }
        }

        public TimeSpan ShiftEnd { get; set; }
        public TimeSpan ShiftStart { get; set; }
    }
}