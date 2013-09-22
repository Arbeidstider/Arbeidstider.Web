using System;
using Arbeidstider.Business.Interfaces.Domain;

namespace Arbeidstider.Business.Domain
{
    public class Timesheet : ITimesheet
    {
        public int EmployerID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime SelectedDay { get; set; }
        public Employer ShiftWorker { get; set; }
        public DateTime Day { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan ShiftEnd { get; set; }
    }
}
