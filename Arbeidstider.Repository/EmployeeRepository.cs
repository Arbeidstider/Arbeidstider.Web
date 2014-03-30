using System;
using System.Collections.Generic;
using System.Linq;
using Arbeidstider.DataObjects.DAO;
using Arbeidstider.Repository.Constants;
using Arbeidstider.Repository.Exceptions;
using Dapper;

namespace Arbeidstider.Repository
{
    public class EmployeeRepository : Repository<EmployeeDAO>
    {
        public EmployeeRepository() : base("Employees")
        {
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<EmployeeDAO> GetAll(object parameters)
        {
            var dt = base.GetMultiple(StoredProcedures.GET_ALL_EMPLOYEES, (DynamicParameters)parameters);
            var employees = dt as EmployeeDAO[] ?? dt.ToArray();

            if (employees == null || !employees.Any())
                throw new EmployeeRepositoryException(string.Format("{0} returned 0 rows.", StoredProcedures.GET_ALL_EMPLOYEES));

            return employees;
        }

        public override int Create(object parameters)
        {
            var dt = base.GetSingle(StoredProcedures.CREATE_EMPLOYEE, (DynamicParameters)parameters);
            // Implement validation

            return dt.Id;
        }

        public override EmployeeDAO Get(object parameters)
        {
            var dt = base.GetSingle(StoredProcedures.GET_EMPLOYEE, (DynamicParameters)parameters);

            return dt;
        }

        public override bool Update(object parameters)
        {
            var dt = base.Execute(StoredProcedures.UPDATE_EMPLOYEE, (DynamicParameters)parameters);

            return true;
        }

        public override bool Exists(object parameters)
        {
            return base.Execute(StoredProcedures.EMPLOYEE_EXISTS, (DynamicParameters)parameters);
        }

        public override bool Delete(object parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}