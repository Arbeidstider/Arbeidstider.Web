using System.Collections.Generic;
using System.Linq;
using Arbeidstider.DataAccess.Domain;
using Arbeidstider.DataAccess.Repository.Constants;
using Arbeidstider.DataAccess.Repository.Exceptions;
using Arbeidstider.Interfaces;

namespace Arbeidstider.DataAccess.Repository
{
    public class TimesheetRepository : IRepository<ITimesheet>
    {
        private readonly IDatabase _database;
        public TimesheetRepository()
        {
            _database = Database.Instance;
        }

        public IEnumerable<ITimesheet> GetAll(object parameters)
        {
            var dt = _database.GetMultiple<Timesheet>(Names.GET_ALL_TIMESHEETS, GetParameters(parameters));
            var timesheets = dt as Timesheet[] ?? dt.ToArray();
            if (dt == null || !timesheets.Any())
                throw new TimesheetRepositoryException(string.Format("Failed to GetAll with parameters: {0}", parameters));

            return timesheets;
        }

        public ITimesheet Create(object parameters)
        {
            var dt = _database.GetSingle<Timesheet>(Names.CREATE_NEW_TIMESHEET, GetParameters(parameters));

            if (dt == null)
                throw new TimesheetRepositoryException("Failed to create new timesheet");

            return dt;
        }

        public ITimesheet Get(object parameters)
        {
            var dt = _database.GetSingle<Timesheet>(Names.GET_TIMESHEET, GetParameters(parameters));
            if (dt == null)
                throw new TimesheetRepositoryException(
                    string.Format("Failed to get timesheet for user with employeeId: {0}", ((IEmployee) parameters).Id));

            return dt;
        }

        public bool Update(object parameters)
        {
            var dt = _database.Execute(Names.UPDATE_TIMESHEET, GetParameters(parameters));

            if (!dt) throw new TimesheetRepositoryException("Failed to update timesheet");

            return true;
        }

        public bool Delete(object parameters)
        {
            return _database.Execute(Names.DELETE_TIMESHEET, GetParameters(parameters));
        }

        public bool Exists(object parameters)
        {
            return true;
        }

        public IEnumerable<KeyValuePair<string, object>> GetParameters(object parameters)
        {
            var p = (ITimesheet) parameters;
            var list = new List<KeyValuePair<string, object>>();
            list.Add(new KeyValuePair<string, object>("UserId", p.UserId));
            list.Add(new KeyValuePair<string, object>("ShiftStart", p.ShiftStart));
            return list;
        }
    }
}