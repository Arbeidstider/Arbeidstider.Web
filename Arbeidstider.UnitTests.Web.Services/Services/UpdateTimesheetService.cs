using System;
using Arbeidstider.UnitTests.Base.Classes;
using Arbeidstider.Web.Services.ServiceModels;
using NUnit.Framework;

namespace Arbeidstider.UnitTests.Web.Services.Services
{
    /// <summary>
    /// Route: /timesheet/update
    /// </summary>
    [TestFixture]
    public class UpdateTimesheetService : TestBase
    {
        /*
        [TestCase]
        public void Update_1()
        {
            var client = GetServiceClient();
            var timesheets = GetTestTimesheets();
            var newDate = DateTime.Now.AddDays(2).Date;
            var newShiftStart = new TimeSpan(7, 0, 0);
            var newShiftEnd = new TimeSpan(15, 0, 0);
            var timesheetID = timesheets[0].TimeSheetID;
            var request = new UpdateTimesheet()
                              {
                                  TimesheetID = timesheetID,
                                  UserID = null,
                                  SelectedDate = newDate,
                                  ShiftStart = newShiftStart,
                                  ShiftEnd = newShiftEnd
                              };
            var response = client.Post<UpdateTimesheetResponse>(request);
            Assert.That(response.TimesheetUpdated == true);

            timesheets = GetTestTimesheets();
            var timesheet = timesheets.Find(x => x.TimeSheetID == timesheetID);
            Console.WriteLine(timesheet.TimeSheetID);
            Console.WriteLine(timesheet.ShiftDate);
            Console.WriteLine(timesheet.ShiftStart);
            Console.WriteLine(timesheet.ShiftEnd);
            Assert.That(DateTime.Parse(timesheet.ShiftDate).Date.Equals(newDate.Date));
            Assert.That(TimeSpan.Parse(timesheet.ShiftStart).Equals(newShiftStart));
            Assert.That(TimeSpan.Parse(timesheet.ShiftEnd).Equals(newShiftEnd));
        }
         */
    }
}
