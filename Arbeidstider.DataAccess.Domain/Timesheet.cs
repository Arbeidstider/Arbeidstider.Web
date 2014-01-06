using System;
using Arbeidstider.Interfaces;

namespace Arbeidstider.DataAccess.Domain
{
    public class Timesheet : ITimesheet
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string Fullname { get; set; }
        public DateTime ShiftDate { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan ShiftEnd { get; set; }
        public IEmployee ShiftWorker { get; set; }
    }
}