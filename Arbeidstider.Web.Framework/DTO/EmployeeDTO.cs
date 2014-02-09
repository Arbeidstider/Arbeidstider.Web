using Arbeidstider.Interfaces;
using Arbeidstider.Web.Framework.Session;

namespace Arbeidstider.Web.Framework.DTO
{
    public class EmployeeDTO
    {
        public EmployeeDTO(EmployeeSession currentUser)
        {
        }
        public EmployeeDTO(IEmployee domain)
        {
            Username = domain.Username;
            WorkplaceId = domain.WorkplaceId;
        }

        public string Username { get; set; }
        public int WorkplaceId { get; set; }
    }
}