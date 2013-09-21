using System;
using Arbeidstider.Business.Interfaces;

namespace Arbeidstider.Web.Services.DTO
{
    public class TimesheetDTO : ITimesheet
    {
        public TimesheetDTO(string shiftStart, string shiftEnd, int employerID, EmployerDTO employer = null)
        {
            ShiftStart = shiftStart;
            ShiftEnd = shiftEnd;
            EmployerID = employerID;
        }

        public TimesheetDTO()
        {
            
        }

        public int EmployerID
        {
            get;
            set;
        }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public DateTime SelectedDay { get; set; }

        public string ShiftStart { get; set; }
        public string ShiftEnd { get; set; }
    }
}
