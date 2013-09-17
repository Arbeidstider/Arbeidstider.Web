using System;
using System.Collections.Generic;
using System.Data;
using Arbeidstider.Business.Domain;
using Arbeidstider.Database;

namespace Arbeidstider.Business.Repository
{
    public class TimesheetRepository : IRepository
    {
        private static TimesheetRepository _instance;

        public static TimesheetRepository Instance
        {
            get
            {
                if (_instance == null)
                    return new TimesheetRepository();

                return _instance;
            }
        }

        private TimesheetRepository() {}

        public IEnumerable<Timesheet> GetAllTimesheets(int employerID, DateTime startDate, DateTime endDate)
        {
            var parameters = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@EmployeeID", employerID),
                new KeyValuePair<string, object>("@StartDate", startDate),
                new KeyValuePair<string, object>("@EndDate", endDate)
            };

            var dt = DatabaseConnection.Instance.ExecuteSP(Arbeidstider.Database.Constants.StoredProcedures.GET_ALL_TIMESHEETS, parameters);
            var timesheets = ParseTimesheets(dt);
            return timesheets;
        }

        public IEnumerable<Timesheet> GetWorkplaceTimesheets(int workplaceID, Employer employer, DateTime startDate, DateTime endDate)
        {
            if (!employer.HasAccessToWorkplace(workplaceID))
            {
                throw new Exception(string.Format("Employer: {0}, does not have access to workplace with ID: {1}", employer.Fullname, workplaceID));
            }

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
                var scheduleStart = (TimeSpan) row["ScheduleEnd"];

                timesheet.ShiftWorker = ParseEmployer(row);
                timesheet.Day = selectedDay;
                timesheet.ShiftEnd = new DateTime(selectedDay.Year, selectedDay.Month, selectedDay.Day, scheduleEnd.Hours, scheduleEnd.Minutes,
                    scheduleEnd.Seconds);
                timesheet.ShiftStart = new DateTime(selectedDay.Year, selectedDay.Month, selectedDay.Day, scheduleStart.Hours, scheduleStart.Minutes,
                    scheduleStart.Seconds);
                timesheets.Add(timesheet);
            }

            return timesheets;
        }

        private static Employer ParseEmployer(DataRow row)
        {
            return new Employer();
        }
    }
}
