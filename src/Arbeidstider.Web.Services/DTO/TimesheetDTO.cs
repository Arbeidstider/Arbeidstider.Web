using System;
using Arbeidstider.Business.Interfaces.Domain;
using Arbeidstider.Business.Interfaces.DTO;

namespace Arbeidstider.Web.Services.DTO
{
    public class TimesheetDTO : ITimesheetDTO
    {
        public TimesheetDTO(ITimesheet timesheet)
        {
            ShiftStart = timesheet.ShiftStart.ToString();
            ShiftEnd = timesheet.ShiftEnd.ToString();
            EmployerID = timesheet.EmployerID;
        }

        public TimesheetDTO() { }

        public int EmployerID { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public DateTime SelectedDay { get; set; }

        public string ShiftStart { get; set; }
        public string ShiftEnd { get; set; }
    }
}
