using System;
using Arbeidstider.Interfaces;

namespace Arbeidstider.DataAccess.Domain
{
    public class Timesheet : ITimesheet
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime ShiftDate { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan ShiftEnd { get; set; }
    }
}