using System;
using Arbeidstider.UnitTests.Base.Classes;
using Arbeidstider.Web.Services.ServiceModels;
using NUnit.Framework;
using ServiceStack;

namespace Arbeidstider.UnitTests.Web.Services.Services
{
    /// <summary>
    /// Route: /timesheet/create
    /// </summary>
    [TestFixture]
    public class CreateTimesheetService : TestBase
    {
        [TestCase]
        public void Create_1()
        {
            var client = new JsonServiceClient("http://localhost:8181");
            var request = new CreateTimesheet()
                                {
                                    SelectedDay = DateTime.Now.Date,
                                    ShiftStart = new TimeSpan(8, 0, 0),
                                    ShiftEnd = new TimeSpan(17, 0, 0),
                                    UserId = 5
                                };

            var response = client.Post(request);
            Assert.That(response.TimesheetCreated == true);
        }

        [TestCase]
        public void Create_2()
        {
            var client = new JsonServiceClient("http://localhost:8181");
            var request = new CreateTimesheet()
                                {
                                    SelectedDay = DateTime.Now.Date.AddDays(1),
                                    ShiftStart = new TimeSpan(10, 0, 0),
                                    ShiftEnd = new TimeSpan(20, 0, 0),
                                    UserId = 5
                                };

            var response = client.Post(request);
            Assert.That(response.TimesheetCreated == true);
        }
    }
}
