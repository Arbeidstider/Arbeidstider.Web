using System;
using Arbeidstider.Business.Logic.Enums;
using Arbeidstider.Common.Parameters;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.Parameters;
using Arbeidstider.Web.Framework.Services;

namespace Arbeidstider.Web.Framework.Scaffolding
{
    public class Data
    {
        public static DateTime[] Dates
        {
            get
            {
                return ScaffoldHelper.GetDates(new DateTime(2013, 10, 1));
            }
        }

        public static TimeSpan[] Times
        {
            get
            {
                return ScaffoldHelper.GetTimes(new TimeSpan(10, 0, 0), new TimeSpan(18, 0, 0));
            }
        }

        public static void Run()
        {
            var employees = EmployeeService.Instance.GetAllEmployees(1);
            foreach (var employee in employees)
            {
                Timesheets.Scaffold(employee.EmployeeID, Dates, Times);
            }
        }
    }
}
