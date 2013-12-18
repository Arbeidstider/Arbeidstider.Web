using System;
using Arbeidstider.UnitTests.Base.Classes;
using Arbeidstider.Web.Services.ServiceModels;
using NUnit.Framework;
using ServiceStack;

namespace Arbeidstider.UnitTests.Web.Services.Services
{
    [TestFixture]
    public class CreateTimesheetService : TestBase
    {
        /* Route: /timesheet/create */
        [TestCase]
        public void Create_1()
        {
            var client = new JsonServiceClient("http://localhost:8181");
            var request = new CreateTimesheet()
                                {
                                    SelectedDay = DateTime.Now.Date,
                                    ShiftStart = new TimeSpan(8, 0, 0),
                                    ShiftEnd = new TimeSpan(17, 0, 0),
                                    UserID = new Guid("62560772-CFD8-4DDB-8CE3-3F37638C4327")
                                };

            var response = client.Post(request);
            Assert.That(response.TimesheetCreated == true);
        }

    }
}
