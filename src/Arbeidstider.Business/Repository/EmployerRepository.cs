using System.Collections.Generic;
using System.Data;
using Arbeidstider.Business.Domain;
using Arbeidstider.Common.Enums;
using Arbeidstider.Database;
using Arbeidstider.Database.Constants;

namespace Arbeidstider.Business.Repository
{
    public class EmployerRepository : IRepository<Employer>
    {
        private static EmployerRepository _instance;

        public static IRepository<Employer> Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EmployerRepository();

                return _instance;
            }
        }

        private EmployerRepository()
        {
            
        }

        public IEnumerable<Employer> GetAll(List<KeyValuePair<string, object>> parameters)
        {
            throw new System.NotImplementedException();
        }

        public Employer Create(List<KeyValuePair<string, object>> parameters)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(Employer obj, List<KeyValuePair<string, object>> parameters)
        {
            throw new System.NotImplementedException();
        }

        public bool Verify(List<KeyValuePair<string, object>> parameters)
        {
            DataTable dt = DatabaseConnection.Instance.ExecuteSP(StoredProcedures.VERIFY_EMPLOYER, parameters);

            DatabaseResult result = (DatabaseResult)(int)dt.Rows[0]["Result"];
            return result == DatabaseResult.SUCCESS;
        }
    }
}
