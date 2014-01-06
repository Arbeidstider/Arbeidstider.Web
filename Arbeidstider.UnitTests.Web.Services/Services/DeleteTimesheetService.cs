using System;
using Arbeidstider.UnitTests.Base.Classes;
using NUnit.Framework;

namespace Arbeidstider.UnitTests.Web.Services.Services
{
    [TestFixture]
    public class DeleteTimesheetService : TestBase
    {
        /*
        [TestCase]
        public void Delete_1()
        {
            var request = new Timesheets()
                                {
                                    StartDate = new DateTime(2013, 9, 1),
                                    EndDate = new DateTime(2013, 12, 31),
                                    UserID = GetTestUserID()
                                };
            var client = GetServiceClient();
            var all = client.Get(request);

            Assert.That(all.Timesheets != null);

            var timesheets = all.Timesheets;

            var deleteRequest = new DeleteTimesheet() {TimesheetID = timesheets[0].TimeSheetID};

            var response = client.Post(deleteRequest);
            Assert.That(response.TimesheetDeleted == true);
        }
         */
    }
}
