using System;

namespace Arbeidstider.Business.Interfaces.Domain
{
    public interface ITimesheet
    {
        Guid UserID { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        DateTime SelectedDay { get; set; }
        TimeSpan ShiftStart { get; set; }
        TimeSpan ShiftEnd { get; set; }
    }
}
