using System.Runtime.Serialization;
using Arbeidstider.Web.Framework.DTO;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    [DataContract]
    [Route("/workinghours/upcoming", "GET")]
    public class UpcomingWorkingHours : IReturn<WorkingHoursDTO>
    {
        [DataMember(Name="EmployeeId")]
        public int EmployeeId { get; set; }
    }
}