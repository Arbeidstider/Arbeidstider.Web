using System;

namespace Arbeidstider.Business.Domain
{
    public class Timesheet : IDomainModel
    {
        public int EmployerID { get; set; }
        public Employer ShiftWorker { get; set; }
        public DateTime Day { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
    }
}
