using System;
using Arbeidstider.DataInterfaces.Repository.Schedule;

namespace Arbeidstider.Repository.Parameters
{
    public class GetAllSchedules : IGetAllSchedules
    {
        public int? EmployeeId { get; set; }
        public int? WorkplaceId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
