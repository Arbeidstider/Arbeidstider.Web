using System;
using Arbeidstider.Business.Interfaces.Domain;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.IoC;
using Arbeidstider.Web.Framework.DTO;

namespace Arbeidstider.Web.Framework.Scaffolding
{
    public class Timesheets
    {
        public static void Scaffold(Guid userID, DateTime[] dates, TimeSpan[] shifts)
        {
            var repository = Container.Resolve<IRepository<ITimesheet>>();
            for (int i = 0; i < dates.Length; i++)
            {
                var uid = userID;
                var selectedDay = dates[i];
                var shiftStart = new TimeSpan();
                var shiftEnd = new TimeSpan();
                shiftStart = shifts[0];
                shiftEnd = shifts[1];
                repository.Create(new TimesheetDTO() { UserID = userID, SelectedDay = selectedDay, ShiftStart = shiftStart, ShiftEnd = shiftEnd}.Parameters());
            }
        }
    }
}