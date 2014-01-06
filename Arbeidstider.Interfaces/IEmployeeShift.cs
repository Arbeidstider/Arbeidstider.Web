using System;

namespace Arbeidstider.DataAccess.Domain
{
    public interface IEmployeeShift
    {
        TimeSpan ShiftEnd { get; }
        TimeSpan ShiftStart { get; }
    }
}