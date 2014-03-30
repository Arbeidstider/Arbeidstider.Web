using System;
using Arbeidstider.DataInterfaces;

namespace Arbeidstider.DataObjects.DAO
{
    public class TimesheetDAO : EntityBase, ITimesheet
    {
        public override int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime ShiftDate { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan ShiftEnd { get; set; }
    }
}