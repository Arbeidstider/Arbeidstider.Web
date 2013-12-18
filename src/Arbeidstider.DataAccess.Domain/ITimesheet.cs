using System;

namespace Arbeidstider.DataAccess.Domain
{
    public interface ITimesheet
    {
        int EmployeeShiftID { get; set; }
        Guid UserID { get; set; }
        DateTime ShiftDate { get; set; }
        TimeSpan ShiftStart { get; set; }
        TimeSpan ShiftEnd { get; set; }
    }
}
