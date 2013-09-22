using System;
using System.Collections.Generic;
using System.Data;
using Arbeidstider.Business.Domain;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Database;

namespace Arbeidstider.Business.Repository
{
    public class TimesheetRepository : IRepository<Timesheet>
    {
        private static TimesheetRepository _instance;

        public static IRepository<Timesheet> Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TimesheetRepository();

                return _instance;
            }
        }

        public IEnumerable<Timesheet> GetAll(List<KeyValuePair<string, object>> parameters)
        {
            throw new NotImplementedException();
        }

        public Timesheet Create(List<KeyValuePair<string, object>> parameters)
        {
            throw new NotImplementedException();
        }

        public Timesheet Get(KeyValuePair<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public bool Update(Timesheet obj, List<KeyValuePair<string, object>> parameters)
        {
            throw new NotImplementedException();
        }

        public bool Verify(List<KeyValuePair<string, object>> parameters)
        {
            return true;
        }

        private TimesheetRepository() {}

        public IEnumerable<Timesheet> GetAllTimesheets(int employerID, DateTime startDate, DateTime endDate)
        {
            var parameters = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@EmployerID", employerID),
                new KeyValuePair<string, object>("@StartDate", startDate),
                new KeyValuePair<string, object>("@EndDate", endDate)
            };

            var dt = DatabaseConnection.Instance.ExecuteSP(Arbeidstider.Database.Constants.StoredProcedures.GET_ALL_TIMESHEETS, parameters);
            var timesheets = ParseTimesheets(dt);
            return timesheets;
        }

        public IEnumerable<Timesheet> GetWorkplaceTimesheets(int workplaceID, Employer employer, DateTime startDate, DateTime endDate)
        {
            /*
            if (!employer.HasAccessToWorkplace(workplaceID))
            {
                throw new Exception(string.Format("Employer: {0}, does not have access to workplace with ID: {1}", employer.Fullname, workplaceID));
            }
             */

            var parameters = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@WorkplaceID", workplaceID),
                new KeyValuePair<string, object>("@StartDate", startDate),
                new KeyValuePair<string, object>("@EndDate", endDate)
            };

            var dt = DatabaseConnection.Instance.ExecuteSP(Arbeidstider.Database.Constants.StoredProcedures.GET_WORKPLACE_TIMESHEETS, parameters);
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

        public bool CreateNewTimesheet(int employerID, DateTime SelectedDay, string shiftStart, string shiftEnd)
        {
            var parameters = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@EmployerID", employerID),
                new KeyValuePair<string, object>("@SelectedDay", SelectedDay),
                new KeyValuePair<string, object>("@ShiftStart", shiftStart),
                new KeyValuePair<string, object>("@ShiftEnd", shiftEnd)
            };

            var dt = DatabaseConnection.Instance.ExecuteSP(Arbeidstider.Database.Constants.StoredProcedures.CREATE_NEW_TIMESHEET, parameters);
            return ParseResult(dt);
        }

        private static bool ParseResult(DataTable dt)
        {
            return true;
        }
    }
}
