using System.Collections.Generic;
using System.Linq;
using Arbeidstider.DataAccess.Domain;
using Arbeidstider.DataAccess.Repository.Constants.StoredProcedures;
using Arbeidstider.DataAccess.Repository.Exceptions;

namespace Arbeidstider.DataAccess.Repository
{
    public class TimesheetRepository : IRepository<ITimesheet>
    {
        private readonly IDatabase _database;
        public TimesheetRepository()
        {
            _database = Database.Instance;
        }

        public IEnumerable<ITimesheet> GetAll(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var dt = _database.GetMultiple<Timesheet>(Names.GET_ALL_TIMESHEETS, parameters);
            var timesheets = dt as Timesheet[] ?? dt.ToArray();
            if (dt == null || !timesheets.Any()) 
                throw new TimesheetRepositoryException(string.Format("Failed to GetAll with parameters: {0}", parameters.ElementAtOrDefault(0).Value));

            return timesheets;
        }

        public bool Create(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var dt = _database.Execute(Names.CREATE_NEW_TIMESHEET, parameters);

            if (!dt)
                throw new TimesheetRepositoryException("Failed to create new timesheet");

            return true;
        }

        public ITimesheet Get(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var dt = _database.GetSingle<Timesheet>(Names.GET_TIMESHEET, parameters);
            if (dt == null) throw new TimesheetRepositoryException(string.Format("Failed to get timesheet for user with userID: {0}", parameters.ElementAtOrDefault(0).Value));

            return dt;
        }

        public bool Update(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var dt = _database.Execute(Names.UPDATE_TIMESHEET, parameters);

            if (!dt) throw new TimesheetRepositoryException("Failed to update timesheet");

            return true;
        }

        public bool Delete(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return _database.Execute(Names.DELETE_TIMESHEET, parameters);
        }

        public bool Exists(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return true;
        }
    }
}