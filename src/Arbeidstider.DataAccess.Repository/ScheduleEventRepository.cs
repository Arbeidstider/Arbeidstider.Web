using System.Collections.Generic;
using Arbeidstider.DataAccess.Domain;
using Arbeidstider.DataAccess.Repository.Constants;
using Arbeidstider.DataAccess.Repository.Constants.StoredProcedures;

namespace Arbeidstider.DataAccess.Repository
{
    public class ScheduleEventRepository : IRepository<ScheduleEvent>
    {
        private readonly IDatabase _database;
        public ScheduleEventRepository()
        {
            _database = Database.Instance;
        }
        public IEnumerable<ScheduleEvent> GetAll(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            throw new System.NotImplementedException();
        }

        public bool Create(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return true;
        }

        public ScheduleEvent Get(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var dt = _database.Execute(Names.UPDATE_TIMESHEET, parameters);
            return dt;
        }

        public bool Exists(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}