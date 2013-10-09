using System;
using Arbeidstider.Web.Framework.DTO;

namespace Arbeidstider.Web.Framework.ViewModels.Timesheet
{
    public class CreateTimesheet
    {
        public CreateTimesheet(TimesheetDTO dto)
        {
        }

        public CreateTimesheet(int employeeID, DateTime weekStart)
        {
        }

        public int EmployeeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime SelectedDay { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan ShiftEnd { get; set; }
    }
}