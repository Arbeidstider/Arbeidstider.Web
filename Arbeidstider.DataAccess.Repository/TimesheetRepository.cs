using System.Collections.Generic;
using System.Linq;
using Arbeidstider.DataAccess.Domain;
using Arbeidstider.DataAccess.Repository.Constants;
using Arbeidstider.DataAccess.Repository.Exceptions;
using Arbeidstider.Interfaces;
using Dapper;

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
            return new List<ITimesheet>();
            //var dt = _database.GetMultiple<Timesheet>(StoredProcedures.GET_ALL_TIMESHEETS, (DynamicParameters)parameters);
            //var timesheets = dt as Timesheet[] ?? dt.ToArray();
            //// try catch
            ////if (dt == null || !timesheets.Any())
            //// throw new TimesheetRepositoryException(string.Format("Failed to GetAll with parameters: {0}", parameters));

            //return timesheets;
        }

        public int Create(object parameters)
        {
            var dt = _database.GetSingle<int>(StoredProcedures.CREATE_TIMESHEET, (DynamicParameters)parameters);

            if (dt == 0)
                throw new TimesheetRepositoryException("Failed to create new timesheet");

            return dt;
        }

        public ITimesheet Get(object parameters)
        {
            var dt = _database.GetSingle<Timesheet>(StoredProcedures.GET_TIMESHEET, (DynamicParameters)parameters);
            if (dt == null)
                throw new TimesheetRepositoryException(
                    string.Format("Failed to get timesheet for user with employeeId: {0}", ((IEmployee)parameters).Id));

            return dt;
        }

        public bool Update(object parameters)
        {
            var dt = _database.Execute(StoredProcedures.UPDATE_TIMESHEET, (DynamicParameters)parameters);

            if (!dt) throw new TimesheetRepositoryException("Failed to update timesheet");

            return true;
        }

        public bool Delete(object parameters)
        {
            return _database.Execute(StoredProcedures.DELETE_TIMESHEET, (DynamicParameters)parameters);
        }

        public bool Exists(object parameters)
        {
            return true;
        }
    }
}