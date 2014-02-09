using System;

namespace Arbeidstider.Interfaces
{
    public interface IEmployeeShift
    {
        int DayOfWeek { get; }
        TimeSpan ShiftEnd { get; }
        TimeSpan ShiftStart { get; }
    }
}