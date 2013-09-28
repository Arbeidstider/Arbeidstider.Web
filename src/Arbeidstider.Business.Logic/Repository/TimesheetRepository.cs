using System;
using System.Collections.Generic;
using System.Data;
using Arbeidstider.Business.Domain;
using Arbeidstider.Business.Interfaces.Database;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.Domain;
using Arbeidstider.Business.Logic.Enums;
using Arbeidstider.Business.Logic.IoC;
using Arbeidstider.Database;

namespace Arbeidstider.Business.Logic.Repository
{
    public class TimesheetRepository : IRepository<Timesheet>
    {
        private readonly IDatabaseConnection _connection;

        public TimesheetRepository()
        {
            _connection = Container.Resolve<IDatabaseConnection>();
        }

        public IEnumerable<Timesheet> GetAll(List<KeyValuePair<string, object>> parameters)
        {
            var dt = DatabaseConnection.Instance.ExecuteSP(Arbeidstider.Database.Constants.StoredProcedures.GET_ALL_TIMESHEETS, parameters);
            var timesheets = ParseTimesheets(dt);
            return timesheets;
        }

        public Timesheet Create(List<KeyValuePair<string, object>> parameters)
        {
            var dt = _connection.ExecuteSP(Database.Constants.StoredProcedures.CREATE_NEW_TIMESHEET, parameters);
            if (dt.Rows.Count == 0|| dt.Rows[0]["Result"] == null || (DatabaseResult) (int) dt.Rows[0]["Result"] == DatabaseResult.FAIL) return null;
            return new Timesheet();
        }

        public Timesheet Get(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            throw new NotImplementedException();
        }

        public bool Update(List<KeyValuePair<string, object>> parameters)
        {
            throw new NotImplementedException();
        }

        public bool Exists(List<KeyValuePair<string, object>> parameters)
        {
            return true;
        }

        public IEnumerable<Timesheet> GetWorkplaceTimesheets(int workplaceID, Employee Employee, DateTime startDate, DateTime endDate)
        {
            /*
            if (!Employee.HasAccessToWorkplace(workplaceID))
            {
                throw new Exception(string.Format("Employee: {0}, does not have access to workplace with ID: {1}", Employee.Fullname, workplaceID));
            }
             */

            var parameters = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@WorkplaceID", workplaceID),
                new KeyValuePair<string, object>("@StartDate", startDate),
                new KeyValuePair<string, object>("@EndDate", endDate)
            };

            var dt = _connection.ExecuteSP(Arbeidstider.Database.Constants.StoredProcedures.GET_WORKPLACE_TIMESHEETS, parameters);
            var timesheets = ParseTimesheets(dt);
            return timesheets;
        }

        private static IEnumerable<Timesheet> ParseTimesheets(DataTable dt)
        {
            var timesheets = new List<Timesheet>();
            foreach (DataRow row in dt.Rows)
            {
                var timesheet = new Timesheet();
                var selectedDay = (DateTime)row["SelectedDay"];
                var scheduleEnd = (TimeSpan) row["ScheduleEnd"];
                var scheduleStart = (TimeSpan) row["ScheduleStart"];

                timesheet.Day = selectedDay;
                timesheet.ShiftEnd = scheduleEnd;
                timesheet.ShiftStart = scheduleStart;
                timesheets.Add(timesheet);
            }

            return timesheets;
        }
    }
}
