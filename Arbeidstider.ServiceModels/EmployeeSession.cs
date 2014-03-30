using System.Runtime.Serialization;
using ServiceStack;

namespace Arbeidstider.ServiceModels
{
    [DataContract]
    public class EmployeeSession : AuthUserSession
    {
        [DataMember]
        public int EmployeeId { get; set; }
        [DataMember]
        public int WorkplaceId { get; set; }
        [DataMember]
        public int?[] Workplaces { get; set; }
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public bool? MobileAppUser { get; set; }
    }
}
