using Arbeidstider.Business.Interfaces.Domain;
using Arbeidstider.Business.Logic.Enums;

namespace Arbeidstider.Web.Framework.ViewModels.Account
{
    public class EmployeeUser : IEmployeeUser
    {
        public EmployeeUser(IEmployee employee)
        {
            WorkplaceID = employee.WorkplaceID;
            Group = employee.EmployeeGroup;
            Fullname = employee.Fullname;
        }

        public EmployeeUser()
        {
            
        }

        public int WorkplaceID { get; set; }
        public int Group { get; set; }
        public string Fullname { get; set; }

        public bool IsAdmin()
        {
            return Group.Equals(EmployeeGroup.Administrator);
        }

        public bool IsManager()
        {
            return Group.Equals(EmployeeGroup.Manager);
        }

        public bool HasAccessToWorkplace(int workplaceID)
        {
            return (IsAdmin() || IsManager()) && WorkplaceID.Equals(workplaceID);
        }
    }
}