using System;
using Arbeidstider.Business.Interfaces.Domain;

namespace Arbeidstider.Web.Framework.ViewModels.Timesheet
{
    public class EmployeeShift : IEmployeeShift
    {
        public EmployeeShift(ITimesheet timesheet)
        {
            ShiftEnd = timesheet.ShiftEnd;
            ShiftStart = timesheet.ShiftStart;
            SelectedDay = timesheet.SelectedDay;
        }

        public DateTime SelectedDay { get; set; }
        public TimeSpan ShiftEnd { get; set; }
        public TimeSpan ShiftStart { get; set; }
    }
}