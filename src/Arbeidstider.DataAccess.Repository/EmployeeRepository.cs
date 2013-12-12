using System.Collections.Generic;
using System.Linq;
using Arbeidstider.DataAccess.Domain;
using Arbeidstider.DataAccess.Repository.Constants;
using Arbeidstider.DataAccess.Repository.Constants.StoredProcedures;
using Arbeidstider.DataAccess.Repository.Exceptions;

namespace Arbeidstider.DataAccess.Repository
{
    public class EmployeeRepository : IRepository<IEmployee>
    {
        private readonly IDatabase _database;
        public EmployeeRepository()
        {
            _database = Database.Instance;
        }

        public IEnumerable<IEmployee> GetAll(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var dt = _database.GetMultiple<Employee>(Names.GET_ALL_EMPLOYEES, parameters);
            var employees = dt as Employee[] ?? dt.ToArray();

            if (employees == null || !employees.Any())
                throw new EmployeeRepositoryException(string.Format("{0} returned 0 rows.", Names.GET_ALL_EMPLOYEES));

            return employees;
        }

        public bool Create(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var dt = _database.Execute(Names.CREATE_EMPLOYEE, parameters);
            if (!dt) 
                throw new EmployeeRepositoryException(string.Format("Failed to create Employee with userID: {0}", parameters.Select(x => x.Key == "UserID").FirstOrDefault().ToString()));

            return true;
        }

        public IEmployee Get(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var dt = _database.GetSingle<Employee>(Names.GET_EMPLOYEE, parameters);

            if (dt == null) throw new EmployeeRepositoryException(string.Format("Failed to get employee with username: {0}", parameters.ElementAtOrDefault(0).Value));

            return dt;
        }

        public bool Update(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var dt = _database.Execute(Names.UPDATE_EMPLOYEE, parameters);

            if (!dt) throw new EmployeeRepositoryException(string.Format("Failed to update employee with employeeID: {0}", parameters.ElementAtOrDefault(0).Value));

            return true;
        }

        public bool Exists(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return _database.Execute(Names.EMPLOYEE_EXISTS, parameters);
        }
    }
}