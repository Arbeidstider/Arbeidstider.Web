using System;
using Arbeidstider.DataInterfaces;

namespace Arbeidstider.DataObjects.DAO
{
    public class ScheduleDAO : EntityBase, ISchedule
    {
        public override int Id { get; set; }
        public int CalendarTypeId { get; set; }
        public int EmployeeId { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan Finish { get; set; }
        public DateTime Date { get; set; }
    }
}
