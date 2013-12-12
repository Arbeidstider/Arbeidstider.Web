using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Arbeidstider.DataAccess.Domain;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.ViewModels.Account;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack;

namespace Arbeidstider.UnitTests.Web.Services.Controllers
{
    [TestClass]
    public class TimesheetController
    {
        public static string PerformTest(string url)
        {
            WebRequest req = WebRequest.Create(url);
            WebResponse resp = req.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

        [TestMethod]
        public void GetAllTimesheets()
        {
            var client = new JsonServiceClient("http://localhost:49646/");
            var requestDto = new TimesheetRequestDTO()
                                 {
                                     StartDate = new DateTime(2013, 9, 1),
                                     EndDate = new DateTime(2013, 12, 31),
                                     UserID = new Guid("62560772-CFD8-4DDB-8CE3-3F37638C4327")
                                 };
            var response = client.Send<IEnumerable<TimesheetDTO>>("GET", "/TimesheetService/GetAllTimesheets", requestDto);
            foreach (var timesheetDto in response)
            {
                Console.WriteLine(timesheetDto.Employee.Fullname);
                Console.WriteLine(timesheetDto.SelectedDay);
                Console.WriteLine(timesheetDto.Shift.ShiftStart);
                Console.WriteLine(timesheetDto.Shift.ShiftEnd);
            }
        }
    }
}
