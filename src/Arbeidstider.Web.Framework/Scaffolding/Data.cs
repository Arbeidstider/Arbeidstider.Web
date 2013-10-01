using System;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.Domain;
using Arbeidstider.Business.Logic.Enums;
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
                return ScaffoldHelper.GetDates(new DateTime(2013, 10, 2));
            }
        }

        public static TimeSpan[] Times
        {
            get
            {
                return ScaffoldHelper.GetTimes(new TimeSpan(8, 0, 0), new TimeSpan(14, 0, 0));
            }
        }

        public static void Run()
        {
            var parameters = new EmployeeParameters(new EmployeeDTO() {WorkplaceID = 1}, RepositoryAction.GetAll);
            var employees = IoC.Resolve<IRepository<Employee>>().GetAll(parameters.Parameters);
            if (employees == null) return;
            foreach (var employee in employees)
            {
                Timesheets.Scaffold(employee.UserID, Dates, Times);
            }
        }
    }
}
