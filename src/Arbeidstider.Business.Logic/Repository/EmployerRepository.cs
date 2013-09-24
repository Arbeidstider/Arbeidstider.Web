﻿using System.Collections.Generic;
using System.Data;
using Arbeidstider.Business.Domain;
using Arbeidstider.Business.Factories;
using Arbeidstider.Business.Interfaces.Database;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.IoC;
using Arbeidstider.Common.Enums;
using Arbeidstider.Database;
using Arbeidstider.Database.Constants;

namespace Arbeidstider.Business.Logic.Repository
{
    public class EmployerRepository : IRepository<Employer>
    {
        private readonly IDatabaseConnection _connection;

        public EmployerRepository()
        {
            _connection = Container.Resolve<IDatabaseConnection>();
        }

        public IEnumerable<Employer> GetAll(List<KeyValuePair<string, object>> parameters)
        {
            throw new System.NotImplementedException();
        }

        public Employer Create(List<KeyValuePair<string, object>> parameters)
        {
            throw new System.NotImplementedException();
        }

        public Employer Get(KeyValuePair<string, object> parameters)
        {
            var dt = DatabaseConnection.Instance.ExecuteSP(StoredProcedures.GET_EMPLOYER, parameters);

            if ((DatabaseResult) (int) dt.Rows[0]["Result"] == DatabaseResult.FAIL || dt.Rows[0] == null)
                return null;
                //throw new EmployerRepositoryException(string.Format("Could not find employer with username: {0}", parameters.Value));

            return EmployerFactory.Create(dt.Rows[0]);
        }


        public bool Update(Employer obj, List<KeyValuePair<string, object>> parameters)
        {
            return true;
        }

        public bool Verify(List<KeyValuePair<string, object>> parameters)
        {
            DataTable dt = _connection.ExecuteSP(StoredProcedures.VERIFY_EMPLOYER, parameters);

            DatabaseResult result = (DatabaseResult)(int)dt.Rows[0]["Result"];
            return result == DatabaseResult.SUCCESS;
        }
    }
}