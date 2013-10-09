using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Arbeidstider.Business.Logic.Domain;
using Arbeidstider.Business.Logic.Repository.Exceptions;

namespace Arbeidstider.Business.Logic.Factories
{
    internal class TimesheetFactory
    {
        internal static Timesheet Create(DataRow row)
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
               throw new TimesheetRepositoryException(string.Format("Factory failed to create timesheet, exception: {0}", ex.Message)); 
            }
        }

        internal static IEnumerable<Timesheet> CreateArray(DataRowCollection rows)
        {
           return (from DataRow row in rows select Create(row)).ToArray();
        }

    }
}