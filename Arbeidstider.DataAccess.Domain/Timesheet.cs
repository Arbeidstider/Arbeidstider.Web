using System;
using Arbeidstider.Interfaces;

namespace Arbeidstider.DataAccess.Domain
{
    public class Timesheet : ITimesheet
    {
        public int EmployeeShiftID { get; set; }
        public Guid UserID { get; set; }
        public DateTime ShiftDate { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan ShiftEnd { get; set; }
        public Employee ShiftWorker { get; set; }
    }
}
