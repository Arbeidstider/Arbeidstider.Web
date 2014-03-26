using System;
using Arbeidstider.Web.Framework.DTO;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    [Route("/timesheets", "POST")]
    [Route("/timesheets/create", "POST")]
    public class CreateTimesheet : IReturn<TimesheetDTO>
    {
        public string SelectedDay { get; set; }
        public string ShiftStart { get; set; }
        public string ShiftEnd { get; set; }
        public int EmployeeId { get; set; }
    }
}