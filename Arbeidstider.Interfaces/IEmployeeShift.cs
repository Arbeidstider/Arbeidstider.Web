using System;

namespace Arbeidstider.Interfaces
{
    public interface IEmployeeShift
    {
        TimeSpan ShiftEnd { get; }
        TimeSpan ShiftStart { get; }
    }
}