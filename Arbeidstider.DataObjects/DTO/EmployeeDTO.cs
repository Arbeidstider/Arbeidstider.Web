using System.Runtime.Serialization;
using Arbeidstider.DataInterfaces;

namespace Arbeidstider.DataObjects.DTO
{
    [DataContract]
    public class EmployeeDTO
    {
        public EmployeeDTO(IEmployee domain)
        {
            userName = domain.Username;
            workplaceId = domain.WorkplaceId;
            id = domain.Id;
        }

        [DataMember]
        public string userName { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int workplaceId { get; set; }
    }
}