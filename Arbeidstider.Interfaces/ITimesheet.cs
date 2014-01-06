using System;

namespace Arbeidstider.Interfaces
{
    public interface ITimesheet
    {
        int Id { get; set; }
        int UserId { get; set; }
        DateTime ShiftDate { get; set; }
        TimeSpan ShiftStart { get; set; }
        TimeSpan ShiftEnd { get; set; }
    }
}
