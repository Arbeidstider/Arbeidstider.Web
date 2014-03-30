using System;

namespace Arbeidstider.DataInterfaces.Repository.Schedule
{
    public interface IGetAllSchedules
    {
        DateTime Start { get; }
        DateTime End { get; }
        int? EmployeeId { get; set; }
        int? WorkplaceId { get; set; }
    }
}
