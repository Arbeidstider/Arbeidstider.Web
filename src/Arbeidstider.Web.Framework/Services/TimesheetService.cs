using System;
using System.Collections.Generic;
using System.Linq;
using Arbeidstider.Business.Domain;
using Arbeidstider.Business.Interfaces.Domain;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.Domain;
using Arbeidstider.Business.Logic.IoC;
using Arbeidstider.Common.Enums;
using Arbeidstider.Common.Parameters;
using Arbeidstider.Web.Framework.ViewModels.Timesheet;

namespace Arbeidstider.Web.Framework.Services
{
    public class TimesheetService
    {
        private readonly IRepository<Timesheet> _repository;
        private static TimesheetService _instance;

        public static TimesheetService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TimesheetService(Container.Resolve<IRepository<Timesheet>>());

                return _instance;
            }
        }

        private TimesheetService(IRepository<Timesheet> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// </summary>
        /// <param name="employerID"></param>
        /// <param name="weekStart">The day that the work week starts, usually monday.</param>
        /// <returns></returns>
        public WeeklyTimesheet GetWeeklyTimesheet(int employeeID, DateTime weekStart)
        {
            var model = new WeeklyTimesheet();
            var parameters =
                new TimesheetParameters(
                    (ITimesheet)(object) (new {EmployeeID = employeeID, StartDate = weekStart, EndDate = weekStart.AddDays(6)}),
                    RepositoryAction.GetAll).Parameters;
            var timesheets = _repository.GetAll(parameters);
            model.Shifts = ParseTimesheetsToShifts(timesheets);

            return model;
        }

        private static IEnumerable<KeyValuePair<DateTime, EmployeeShift>> ParseTimesheetsToShifts(IEnumerable<Timesheet> timesheets)
        {
            var shifts = new List<KeyValuePair<DateTime, EmployeeShift>>();
            foreach (var timesheet in timesheets)
            {
                shifts.Add(new KeyValuePair<DateTime, EmployeeShift>(timesheet.SelectedDay,
                    new EmployeeShift(timesheet.ShiftStart, timesheet.ShiftEnd)));
            }

            return shifts.OrderBy(x => x.Key).ToArray();
        }
    }
}
