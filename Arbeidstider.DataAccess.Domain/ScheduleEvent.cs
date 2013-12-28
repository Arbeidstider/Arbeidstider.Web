using System;

namespace Arbeidstider.DataAccess.Domain
{
    public class ScheduleEvent
    {
        public ScheduleEvent() {}
        public int EmployeeScheduleEventD { get; set; }
        public DateTime SelectedDay { get; set; }
        public IEmployeeShift Shift { get; set; }
    }
}
