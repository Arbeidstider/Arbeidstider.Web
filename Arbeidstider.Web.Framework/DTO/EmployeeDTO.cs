using Arbeidstider.DataAccess.Domain;
using Arbeidstider.Web.Framework.Session;

namespace Arbeidstider.Web.Framework.DTO
{
    public class EmployeeDTO
    {
        public EmployeeDTO(IEmployeeSession currentUser)
        {
        }
        public EmployeeDTO(IEmployee domain)
        {
            Username = domain.Username;
        }
        public string Username { get; set; }
    }
}