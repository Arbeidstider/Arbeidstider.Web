using System.Collections.Generic;
using Arbeidstider.DataAccess.Domain;
using Arbeidstider.DataAccess.Repository.Constants;
using Arbeidstider.Interfaces;

namespace Arbeidstider.DataAccess.Repository
{
    public class ScheduleEventRepository : IRepository<ScheduleEvent>
    {
        private readonly IDatabase _database;
        public ScheduleEventRepository()
        {
            _database = Database.Instance;
        }
        public IEnumerable<ScheduleEvent> GetAll(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public ScheduleEvent Create(object parameters)
        {
            return new ScheduleEvent();
        }

        public ScheduleEvent Get(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(object parameters)
        {
            var dt = _database.Execute(Names.UPDATE_TIMESHEET, GetParameters(parameters));
            return dt;
        }

        public bool Exists(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<KeyValuePair<string, object>> GetParameters(object parameters)
        {
            var list = new List<KeyValuePair<string, object>>();
            return list;
        }
    }
}