using System;

namespace Arbeidstider.DataInterfaces
{
    public interface IEmployeeShift
    {
        int DayOfWeek { get; set; }
        DateTime? ShiftDate { get; set; }
        int? EmployeeId { get; }
        TimeSpan ShiftEnd { get; }
        TimeSpan ShiftStart { get; }
    }
}