using System;

namespace Arbeidstider.Business.Logic.Domain
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
