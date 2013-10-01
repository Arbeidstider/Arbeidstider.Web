using System;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.Domain;
using Arbeidstider.Business.Logic.Enums;
using Arbeidstider.Business.Logic.IoC;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.Parameters;

namespace Arbeidstider.Web.Framework.Scaffolding
{
    public class Timesheets
    {
        public static void Scaffold(Guid userID, DateTime[] dates, TimeSpan[] shifts)
        {
            var repository = Container.Resolve<IRepository<Timesheet>>();
            for (int i = 0; i < dates.Length; i++)
            {
                TimesheetDTO dto = new TimesheetDTO();
                var uid = userID;
                var selectedDay = dates[i];
                var shiftStart = new TimeSpan();
                var shiftEnd = new TimeSpan();
                if (i%2 == 0)
                {
                    shiftStart = shifts[0];
                    shiftEnd = shifts[1];
                }
                else
                {
                    shiftStart = shifts[2];
                    shiftEnd = shifts[3];
                }
                var parameters = new TimesheetParameters(uid, selectedDay, shiftStart, shiftEnd, RepositoryAction.Create);
                repository.Create(parameters.Parameters);
            }
        }
    }
}