using System.Runtime.Serialization;
using Arbeidstider.DataObjects.DTO;
using ServiceStack;

namespace Arbeidstider.ServiceModels
{
    [DataContract]
    [Route("/workinghours/upcoming", "GET")]
    public class UpcomingWorkingHours : IReturn<WorkingHoursDTO>
    {
        [DataMember(Name="EmployeeId")]
        public int EmployeeId { get; set; }
    }
}