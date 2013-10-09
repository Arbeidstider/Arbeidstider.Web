﻿using System.Collections.Generic;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.Domain;

namespace Arbeidstider.Business.Logic.Repository
{
    public class ScheduleEventRepository : RepositoryBase, IRepository<ScheduleEvent>
    {
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
            var dt = Database.ExecuteSP(Constants.StoredProcedures.UPDATE_TIMESHEET, parameters);
            return true;
        }

        public bool Exists(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}
