using System.Collections.Generic;
using System.Linq;
using Arbeidstider.Business.Interfaces.Database;
using Arbeidstider.Business.Interfaces.Domain;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.Factories;
using Arbeidstider.Business.Logic.Repository.Constants;
using Arbeidstider.Business.Logic.Repository.Exceptions;

namespace Arbeidstider.Business.Logic.Repository
{
    public class TimesheetRepository : RepositoryBase, IRepository<ITimesheet>
    {
        private readonly IDatabaseConnection _connection;
        public TimesheetRepository(IDatabaseConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<ITimesheet> GetAll(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var dt = Database.ExecuteSP(StoredProcedures.GET_ALL_TIMESHEETS, parameters);
            if (!dt.QueryExecutedSuccessfully()) 
                throw new TimesheetRepositoryException(string.Format("Failed to GetAll with parameters: {0}", parameters.ElementAtOrDefault(0).Value));

            return TimesheetFactory.CreateArray(dt.Rows);
        }

        public bool Create(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var dt = Database.ExecuteSP(StoredProcedures.CREATE_NEW_TIMESHEET, parameters);

            if (!dt.QueryExecutedSuccessfully())
                throw new TimesheetRepositoryException("Failed to create new timesheet");

            return true;
        }

        public ITimesheet Get(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var dt = Database.ExecuteSP(StoredProcedures.GET_TIMESHEET, parameters);
            if (!dt.QueryExecutedSuccessfully()) throw new TimesheetRepositoryException(string.Format("Failed to get timesheet for user with userID: {0}", parameters.ElementAtOrDefault(0).Value));

            return TimesheetFactory.Create(dt.Rows[0]);
        }

        public bool Update(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var dt = Database.ExecuteSP(StoredProcedures.UPDATE_TIMESHEET, parameters);

            if (!dt.QueryExecutedSuccessfully()) throw new TimesheetRepositoryException("Failed to update timesheet");

            return true;
        }

        public bool Exists(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return true;
        }
    }
}