using System.Collections.Generic;
using System.Data;
using System.Linq;
using Arbeidstider.Business.Interfaces.Database;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.Domain;
using Arbeidstider.Business.Logic.Enums;
using Arbeidstider.Business.Logic.Factories;
using Arbeidstider.Business.Logic.IoC;
using Arbeidstider.Business.Logic.Repository.Exceptions;
using Arbeidstider.Database.Constants;

namespace Arbeidstider.Business.Logic.Repository
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly IDatabaseConnection _connection;

        public EmployeeRepository()
        {
            _connection = Container.Resolve<IDatabaseConnection>();
        }

        public IEnumerable<Employee> GetAll(List<KeyValuePair<string, object>> parameters)
        {
            var dt = _connection.ExecuteSP(StoredProcedures.GET_ALL_EMPLOYEES, parameters);

            if (!dt.QueryExecutedSuccessfully()) 
                throw new EmployeeRepositoryException(string.Format("{0} returned 0 rows.", StoredProcedures.GET_ALL_EMPLOYEES));

            return EmployeeFactory.CreateArray(dt.Rows);
        }

        public Employee Create(List<KeyValuePair<string, object>> parameters)
        {
            var dt = _connection.ExecuteSP(StoredProcedures.CREATE_EMPLOYEE, parameters);
            if (!dt.QueryExecutedSuccessfully()) return null;

            return EmployeeFactory.Create(dt.Rows[0]);
        }

        public Employee Get(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var dt = _connection.ExecuteSP(StoredProcedures.GET_EMPLOYEE, parameters);

            if (!dt.QueryExecutedSuccessfully()) throw new EmployeeRepositoryException(string.Format("Failed to get employee with username: {0}", parameters.ElementAt(0).Value));

            return EmployeeFactory.Create(dt.Rows[0]);
        }

        public bool Update(List<KeyValuePair<string, object>> parameters)
        {
            var dt = _connection.ExecuteSP(StoredProcedures.UPDATE_EMPLOYEE, parameters);

            return true;
        }

        public bool Exists(List<KeyValuePair<string, object>> parameters)
        {
            DataTable dt = _connection.ExecuteSP(StoredProcedures.EMPLOYEE_EXISTS, parameters);

            DatabaseResult result = (DatabaseResult)(int)dt.Rows[0]["Result"];
            return result == DatabaseResult.SUCCESS;
        }
    }
}