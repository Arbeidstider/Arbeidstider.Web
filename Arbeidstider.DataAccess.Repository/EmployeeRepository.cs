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
            var dt = _database.GetMultiple<Employee>(Names.GET_ALL_EMPLOYEES, GetParameters(parameters));
            var employees = dt as Employee[] ?? dt.ToArray();

            if (employees == null || !employees.Any())
                throw new EmployeeRepositoryException(string.Format("{0} returned 0 rows.", Names.GET_ALL_EMPLOYEES));

            return employees;
        }

        public IEmployee Create(object parameters)
        {
            var dt = _database.GetSingle<Employee>(Names.CREATE_EMPLOYEE, GetParameters(parameters));
            if (dt.Id == 0)
                throw new EmployeeRepositoryException(string.Format("Failed to create Employee with userId: {0}",
                    ((IEmployee)(object)parameters).UserId));

            return dt;
        }

        public IEmployee Get(object parameters)
        {
            var dt = _database.GetSingle<Employee>(Names.GET_EMPLOYEE, GetParameters(parameters));

            if (dt == null)
                throw new EmployeeRepositoryException(string.Format("Failed to get employee with username: {0}",
                                                                    ((IEmployee)(object)parameters).Username));

            return dt;
        }

        public bool Update(object parameters)
        {
            var dt = _database.Execute(Names.UPDATE_EMPLOYEE, GetParameters(parameters));

            if (!dt)
                throw new EmployeeRepositoryException(string.Format("Failed to update employee with employeeId: {0}",
                                                                    ((IEmployee)(object)parameters).Id));

            return true;
        }

        public bool Exists(object parameters)
        {
            return _database.Execute(Names.EMPLOYEE_EXISTS, GetParameters(parameters));
        }

        public bool Delete(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public DynamicParameters GetParameters(object parameters)
        {
            var list = new DynamicParameters();
            var p = (IEmployeeParameters) parameters;
            if (p.Id)
            list.Add(new KeyValuePair<string, object>("Id", p.Id));
            list.Add(new KeyValuePair<string, object>("UserId", p.UserId));
            list.Add(new KeyValuePair<string, object>("WorkplaceId", p.WorkplaceId));
            list.Add(new KeyValuePair<string, object>("Username", p.Username));
            return list;
        }
    }
}