using System;
using Arbeidstider.Business.Domain;
using Arbeidstider.Business.Interfaces.Domain;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.Enums;
using Arbeidstider.Business.Logic.IoC;
using Arbeidstider.Common.Parameters;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.Parameters;

namespace Arbeidstider.Web.Framework.Scaffolding
{
    public class Timesheets
    {
        public static void Scaffold(int EmployeeID, DateTime[] dates, TimeSpan[] shifts)
        {
            var repository = Container.Resolve<IRepository<Timesheet>>();
            for (int i = 0; i < dates.Length; i++)
            {
                TimesheetDTO dto = new TimesheetDTO();
                dto.EmployeeID = 7;
                dto.SelectedDay = dates[i].ToString();
                if (i%2 == 0)
                {
                    dto.ShiftStart = shifts[0].ToString();
                    dto.ShiftEnd = shifts[1].ToString();
                }
                else
                {
                    dto.ShiftStart = shifts[2].ToString();
                    dto.ShiftEnd = shifts[3].ToString();
                }
                var parameters = new TimesheetParameters(dto, RepositoryAction.Create);
                repository.Create(parameters.Parameters);
            }
        }
    }
}