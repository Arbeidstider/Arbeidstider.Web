using Arbeidstider.Business.Logic.Domain;
using Arbeidstider.Business.Logic.Enums;
using Arbeidstider.Web.Framework.DTO;

namespace Arbeidstider.Web.Framework.ViewModels.Account
{
    public class EmployeeUser : EmployeeDTO
    {
        public EmployeeUser(Employee employee) : base(employee)
        {
        }

        public EmployeeUser()
        {
            
        }

        public bool IsAdmin()
        {
            return base.Group.Equals(EmployeeGroup.Administrator);
        }

        public bool IsManager()
        {
            return base.Group.Equals(EmployeeGroup.Manager);
        }

        public bool HasAccessToWorkplace(int workplaceID)
        {
            return (IsAdmin() || IsManager()) && base.WorkplaceID.Equals(workplaceID);
        }
    }
}