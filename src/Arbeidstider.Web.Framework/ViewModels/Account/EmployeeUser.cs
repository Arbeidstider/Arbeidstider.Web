using Arbeidstider.DataAccess.Domain;

namespace Arbeidstider.Web.Framework.ViewModels.Account
{
    public class EmployeeUser
    {
        private readonly int _group;
        private readonly int _workplaceID;
        private readonly string _fullname;

        public EmployeeUser(IEmployee employee)
        {
            _group = employee.EmployeeGroup;
            _workplaceID = employee.WorkplaceID;
            _fullname = employee.Fullname;
        }

        public bool IsAdmin()
        {
            return _group.Equals((int)1);
        }

        public bool IsManager()
        {
            return _group.Equals((int)2);
        }

        public bool HasAccessToWorkplace(int workplaceID)
        {
            return (IsAdmin() || (IsManager()) && _workplaceID.Equals(workplaceID));
        }
        
        public int WorkplaceID
        {
             get { return _workplaceID; }
        }

        public string Fullname
        {
            get { return _fullname; }
        }
    }
}