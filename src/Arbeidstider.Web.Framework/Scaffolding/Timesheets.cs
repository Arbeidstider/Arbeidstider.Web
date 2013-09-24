using System;
using Arbeidstider.Business.Domain;
using Arbeidstider.Business.Interfaces.Domain;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.IoC;
using Arbeidstider.Common.Enums;
using Arbeidstider.Common.Parameters;
using Autofac;

namespace Arbeidstider.Web.Framework.Scaffolding
{
    public class Timesheets
    {
        public static void Scaffold(int employerID, DateTime[] dates, TimeSpan[] shifts)
        {
            var repository = Container.BaseContainer.Resolve<IRepository<Timesheet>>();
            for (int i = 0; i < dates.Length; i++)
            {
                ITimesheet dto = new Timesheet();
                dto.EmployerID = 7;
                dto.SelectedDay = dates[i];
                if (i%2 == 0)
                {
                    dto.ShiftStart = shifts[0];
                    dto.ShiftEnd = shifts[1];
                }
                else
                {
                    dto.ShiftStart = shifts[2];
                    dto.ShiftEnd = shifts[3];
                }
                var parameters = new TimesheetParameters(dto, RepositoryAction.Create);
                repository.Create(parameters.Parameters);
            }
        }
    }
}