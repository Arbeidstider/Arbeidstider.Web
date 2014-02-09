using System;
using Arbeidstider.Interfaces;

namespace Arbeidstider.DataAccess.Domain
{
    public class EmployeeShift : IEmployeeShift
    {
        private readonly TimeSpan _shiftEnd;
        private readonly TimeSpan _shiftStart;
        private readonly int _dayOfWeek;
        public EmployeeShift(ITimesheet timesheet)
        {
            _dayOfWeek = (int)timesheet.ShiftDate.DayOfWeek;
            _shiftEnd = timesheet.ShiftEnd;
            _shiftStart = timesheet.ShiftStart;
        }

        public int DayOfWeek { get { return _dayOfWeek; } }
        public TimeSpan ShiftEnd { get { return _shiftEnd; } }
        public TimeSpan ShiftStart { get { return _shiftStart; } }
    }
}