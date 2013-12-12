using System;
namespace Arbeidstider.DataAccess.Domain
{
    public class Timesheet : ITimesheet
    {
        public Guid UserID { get; set; } 
        public int EmployeeScheduleEventID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime SelectedDay { get; set; }
        public Employee ShiftWorker { get; set; }
        public DateTime Day { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan ShiftEnd { get; set; }
    }
}
