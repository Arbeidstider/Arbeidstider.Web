using System;
using Arbeidstider.UnitTests.Base.Classes;
using Arbeidstider.Web.Services.ServiceModels;
using NUnit.Framework;

namespace Arbeidstider.UnitTests.Web.Services.Services
{
    [TestFixture]
    public class TimesheetsService : TestBase
    {
        /* Route: /timesheets */
        [TestCase]
        public void GetAll_1()
        {
            var client = GetServiceClient();
            var request = new Timesheets()
                                {
                                    StartDate = new DateTime(2013, 9, 1),
                                    EndDate = new DateTime(2013, 12, 31),
                                    UserID = new Guid("62560772-CFD8-4DDB-8CE3-3F37638C4327")
                                };

            var all = client.Get(request);
            Assert.That(all.Timesheets != null);
            foreach (var timesheetDto in all.Timesheets)
            {
                Console.WriteLine(timesheetDto.UserID);
                Console.WriteLine(timesheetDto.ShiftDate);
                Console.WriteLine(timesheetDto.ShiftStart);
                Console.WriteLine(timesheetDto.ShiftEnd);
            }
        }
    }
}
