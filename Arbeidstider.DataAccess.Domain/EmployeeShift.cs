using System;
using Arbeidstider.Interfaces;

namespace Arbeidstider.DataAccess.Domain
{
    public class EmployeeShift : IEmployeeShift
    {
        private readonly TimeSpan _shiftEnd;
        private readonly TimeSpan _shiftStart;
        public EmployeeShift(ITimesheet timesheet)
        {
            _shiftEnd = timesheet.ShiftEnd;
            _shiftStart = timesheet.ShiftStart;
        }

        public TimeSpan ShiftEnd { get { return _shiftEnd; } }
        public TimeSpan ShiftStart { get { return _shiftStart; } }
    }
}