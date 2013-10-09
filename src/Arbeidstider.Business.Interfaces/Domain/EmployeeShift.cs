using System;

namespace Arbeidstider.Business.Interfaces.Domain
{
    public interface IEmployeeShift
    {
        TimeSpan ShiftEnd { get; set; }
        TimeSpan ShiftStart { get; set; }
    }
}
