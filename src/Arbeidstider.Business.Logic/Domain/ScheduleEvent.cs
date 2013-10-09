using System;
using Arbeidstider.Business.Interfaces.Domain;

namespace Arbeidstider.Business.Logic.Domain
{
    public class ScheduleEvent
    {
        public ScheduleEvent() {}
        public int EmployeeScheduleEventD { get; set; }
        public DateTime SelectedDay { get; set; }
        public IEmployeeShift Shift { get; set; }
    }
}
