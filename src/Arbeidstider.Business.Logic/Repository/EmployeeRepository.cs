using System.Collections.Generic;
using System.Data;
using System.Linq;
using Arbeidstider.Business.Interfaces.Database;
using Arbeidstider.Business.Interfaces.Domain;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.Factories;
using Arbeidstider.Business.Logic.Repository.Constants;
using Arbeidstider.Business.Logic.Repository.Exceptions;

namespace Arbeidstider.Business.Logic.Repository
{
    public class EmployeeRepository : IRepository<IEmployee>
    {
        private readonly IDatabaseConnection _connection;

        public EmployeeRepository(IDatabaseConnection connnection)
        {
            _connection = connnection;
        }

        public IEnumerable<IEmployee> GetAll(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var dt = _connection.ExecuteSP(StoredProcedures.GET_ALL_EMPLOYEES, parameters);

            if (!dt.QueryExecutedSuccessfully()) 
                throw new EmployeeRepositoryException(string.Format("{0} returned 0 rows.", StoredProcedures.GET_ALL_EMPLOYEES));

            return EmployeeFactory.CreateArray(dt.Rows);
        }

        public bool Create(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var dt = _connection.ExecuteSP(StoredProcedures.CREATE_EMPLOYEE, parameters);
            if (!dt.QueryExecutedSuccessfully()) throw new EmployeeRepositoryException(string.Format("Failed to create Employee with userID: {0}", parameters.Select(x => x.Key == "UserID").FirstOrDefault().ToString()));

            return true;
        }

        public IEmployee Get(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var dt = _connection.ExecuteSP(StoredProcedures.GET_EMPLOYEE, parameters);

            if (!dt.QueryExecutedSuccessfully()) throw new EmployeeRepositoryException(string.Format("Failed to get employee with username: {0}", parameters.ElementAtOrDefault(0).Value));

            return EmployeeFactory.Create(dt.Rows[0]);
        }

        public bool Update(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var dt = _connection.ExecuteSP(StoredProcedures.UPDATE_EMPLOYEE, parameters);

            if (!dt.QueryExecutedSuccessfully()) throw new EmployeeRepositoryException(string.Format("Failed to update employee with employeeID: {0}", parameters.ElementAtOrDefault(0).Value));

            return true;
        }

        public bool Exists(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            DataTable dt = _connection.ExecuteSP(StoredProcedures.EMPLOYEE_EXISTS, parameters);

            return dt.QueryExecutedSuccessfully();
        }
    }
}