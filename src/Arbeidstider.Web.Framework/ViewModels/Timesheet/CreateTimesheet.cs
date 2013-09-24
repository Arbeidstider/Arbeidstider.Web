using System;
using Arbeidstider.Business.Interfaces.Domain;
using Arbeidstider.Common.DTO;

namespace Arbeidstider.Web.Framework.ViewModels.Timesheet
{
    public class CreateTimesheet : ITimesheet
    {
        public CreateTimesheet(TimesheetDTO dto)
        {
            EmployerID = dto.EmployerID;
            SelectedDay = DateTime.Parse(dto.SelectedDay);
            ShiftStart = TimeSpan.Parse(dto.ShiftStart);
            ShiftEnd = TimeSpan.Parse(dto.ShiftEnd);
        }
        public int EmployerID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime SelectedDay { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan ShiftEnd { get; set; }
    }
}