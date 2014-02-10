using System.Collections.Generic;
using System.Linq;
using Arbeidstider.DataAccess.Domain;
using Arbeidstider.DataAccess.Repository.Constants;
using Arbeidstider.DataAccess.Repository.Exceptions;
using Arbeidstider.Interfaces;
using Dapper;

namespace Arbeidstider.DataAccess.Repository
{
    public class EmployeeRepository : IRepository<IEmployee>
    {
        private readonly IDatabase _database;

        public EmployeeRepository()
        {
            _database = Database.Instance;
        }

        public IEnumerable<IEmployee> GetAll(object parameters)
        {
            var dt = _database.GetMultiple<Employee>(StoredProcedures.GET_ALL_EMPLOYEES, (DynamicParameters)parameters);
            var employees = dt as Employee[] ?? dt.ToArray();

            if (employees == null || !employees.Any())
                throw new EmployeeRepositoryException(string.Format("{0} returned 0 rows.", StoredProcedures.GET_ALL_EMPLOYEES));

            return employees;
        }

        public int Create(object parameters)
        {
            var dt = _database.GetSingle<int>(StoredProcedures.CREATE_EMPLOYEE, (DynamicParameters)parameters);
            if (dt == 0)
                throw new EmployeeRepositoryException(string.Format("Failed to create Employee with userId: {0}",
                    ((IEmployee)(object)parameters).UserId));

            return dt;
        }

        public IEmployee Get(object parameters)
        {
            var dt = _database.GetSingle<Employee>(StoredProcedures.GET_EMPLOYEE, (DynamicParameters)parameters);

            if (dt == null)
                throw new EmployeeRepositoryException(string.Format("Failed to get employee with username: {0}",
                                                                    ((DynamicParameters)parameters).Get<string>("Username")));

            return dt;
        }

        public bool Update(object parameters)
        {
            var dt = _database.Execute(StoredProcedures.UPDATE_EMPLOYEE, (DynamicParameters)parameters);

            if (!dt)
                throw new EmployeeRepositoryException(string.Format("Failed to update employee with employeeId: {0}",
                                                                    ((IEmployee)(object)parameters).Id));

            return true;
        }

        public bool Exists(object parameters)
        {
            return _database.Execute(StoredProcedures.EMPLOYEE_EXISTS, (DynamicParameters)parameters);
        }

        public bool Delete(object parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}