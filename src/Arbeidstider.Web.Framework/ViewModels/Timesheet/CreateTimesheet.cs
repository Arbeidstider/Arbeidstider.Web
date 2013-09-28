using System;
using Arbeidstider.Business.Interfaces.Domain;
using Arbeidstider.Web.Framework.DTO;

namespace Arbeidstider.Web.Framework.ViewModels.Timesheet
{
    public class CreateTimesheet : ITimesheet
    {
        public CreateTimesheet(TimesheetDTO dto)
        {
            EmployeeID = dto.EmployeeID;
            SelectedDay = DateTime.Parse(dto.SelectedDay);
            ShiftStart = TimeSpan.Parse(dto.ShiftStart);
            ShiftEnd = TimeSpan.Parse(dto.ShiftEnd);
        }

        public CreateTimesheet(int employeeID, DateTime weekStart)
        {
            EmployeeID = employeeID;
            StartDate = weekStart;
            EndDate = weekStart.AddDays(6);
        }

        public int EmployeeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime SelectedDay { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan ShiftEnd { get; set; }
    }
}