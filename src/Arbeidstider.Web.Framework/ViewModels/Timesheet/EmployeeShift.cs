using System;

namespace Arbeidstider.Web.Framework.ViewModels.Timesheet
{
    public class EmployeeShift
    {
        public EmployeeShift(TimeSpan shiftStart, TimeSpan shiftEnd)
        {
            ShiftEnd = shiftEnd;
            ShiftStart = shiftStart;
        }
        public TimeSpan ShiftEnd { get; set; }
        public TimeSpan ShiftStart { get; set; }
    }
}
