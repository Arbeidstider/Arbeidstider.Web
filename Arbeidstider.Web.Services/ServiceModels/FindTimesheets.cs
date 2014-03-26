using System.Collections.Generic;
using Arbeidstider.Web.Framework.DTO;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    [Route("/timesheets/search", "GET")]
    public class FindTimesheets : IReturn<IEnumerable<TimesheetDTO>>
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? EmployeeId { get; set; }
        public int? WorkplaceId { get; set; }
        public bool? WeeklyView { get; set; }
    }
}