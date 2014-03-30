using Arbeidstider.DataObjects.DTO;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    [Route("/timesheets", "PUT PATCH")]
    [Route("/timesheets/update", "PUT PATCH")]
    public class UpdateTimesheets : IReturn<TimesheetDTO>
    {
        /* EmployeeScheduleEventID in DB */
        public int Id { get; set; }

        /* Change ShiftWorker */
        public int? EmployeeId { get; set; }

        /* Change Day / Time */
        public string Date { get; set; }
        public string ShiftEnd { get; set; }
        public string ShiftStart { get; set; }
    }
}