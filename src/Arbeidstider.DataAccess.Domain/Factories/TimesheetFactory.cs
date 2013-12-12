using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Arbeidstider.DataAccess.Domain.Factories
{
    public class TimesheetFactory
    {
        public static Timesheet Create(DataRow row)
        {
            try
            {
                var timesheet = new Timesheet();
                var selectedDay = DateTime.Parse(row["SelectedDay"].ToString()).Date;
                var scheduleEnd = (TimeSpan) row["ScheduleEnd"];
                var scheduleStart = (TimeSpan) row["ScheduleStart"];
                if (row["EmployeeScheduleEventID"] != null) timesheet.EmployeeScheduleEventID = (int) row["EmployeeScheduleEventID"];
                timesheet.SelectedDay = selectedDay;
                timesheet.Day = selectedDay;
                timesheet.UserID = (Guid)row["userID"];
                timesheet.ShiftEnd = scheduleEnd;
                timesheet.ShiftStart = scheduleStart;
                return timesheet;
            }
            catch (Exception ex)
            {
               throw new Exception(string.Format("Factory failed to create timesheet, exception: {0}", ex.Message)); 
            }
        }

        public static IEnumerable<Timesheet> CreateArray(DataRowCollection rows)
        {
           return (from DataRow row in rows select Create(row)).ToArray();
        }
    }
}