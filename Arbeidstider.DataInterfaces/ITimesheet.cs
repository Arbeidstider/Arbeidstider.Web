using System;

namespace Arbeidstider.DataInterfaces
{
    public interface ITimesheet : IDataObject
    {
        int Id { get; set; }
        int EmployeeId { get; set; }
        DateTime ShiftDate { get; set; }
        TimeSpan ShiftStart { get; set; }
        TimeSpan ShiftEnd { get; set; }
    }
}
