using System;
using System.Collections.Generic;
using System.Linq;
using Arbeidstider.Business.Interfaces.Database;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.Domain;
using Arbeidstider.Business.Logic.Factories;
using Arbeidstider.Business.Logic.IoC;
using Arbeidstider.Business.Logic.Repository.Exceptions;

namespace Arbeidstider.Business.Logic.Repository
{
    public class TimesheetRepository : IRepository<Timesheet>
    {
        private readonly IDatabaseConnection _connection;

        public TimesheetRepository()
        {
            _connection = Container.Resolve<IDatabaseConnection>();
        }

        public IEnumerable<Timesheet> GetAll(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var dt = _connection.ExecuteSP(Database.Constants.StoredProcedures.GET_ALL_TIMESHEETS, parameters);
            if (!dt.QueryExecutedSuccessfully()) 
                throw new TimesheetRepositoryException(string.Format("Failed to GetAll with parameters: {0}", parameters.ElementAtOrDefault(0).Value));

            return TimesheetFactory.CreateArray(dt.Rows);
        }

        public bool Create(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var dt = _connection.ExecuteSP(Database.Constants.StoredProcedures.CREATE_NEW_TIMESHEET, parameters);

            if (!dt.QueryExecutedSuccessfully())
                throw new TimesheetRepositoryException("Failed to create new timesheet");

            return true;
        }

        public Timesheet Get(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            throw new NotImplementedException();
        }

        public bool Update(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            throw new NotImplementedException();
        }

        public bool Exists(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return true;
        }

        public IEnumerable<Timesheet> GetWorkplaceTimesheets(int workplaceID, Employee Employee, DateTime startDate, DateTime endDate)
        {
            var parameters = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@WorkplaceID", workplaceID),
                new KeyValuePair<string, object>("@StartDate", startDate),
                new KeyValuePair<string, object>("@EndDate", endDate)
            };

            var dt = _connection.ExecuteSP(Database.Constants.StoredProcedures.GET_WORKPLACE_TIMESHEETS, parameters);
            return TimesheetFactory.CreateArray(dt.Rows);
        }
    }
}