using System;
using Arbeidstider.Web.Framework.ViewModels.Timesheet;

namespace Arbeidstider.Business.Logic.Domain
{
    public class ScheduleEvent
    {
        public ScheduleEvent() {}
        public int EmployeeScheduleEventD { get; set; }
        public DateTime SelectedDay { get; set; }
        public EmployeeShift Shift { get; set; }
    }
}
