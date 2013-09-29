using System;
using System.Collections.Generic;
using System.Linq;
using Arbeidstider.Business.Domain;
using Arbeidstider.Business.Interfaces.Caching;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.Domain;
using Arbeidstider.Business.Logic.Enums;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.Parameters;
using Arbeidstider.Web.Framework.ViewModels.Timesheet;

namespace Arbeidstider.Web.Framework.Services
{
    public class TimesheetService
    {
        private readonly ICacheService _cacheService; 
        private readonly IRepository<Timesheet> _repository;
        private static TimesheetService _instance;

        public static TimesheetService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TimesheetService(IoC.Resolve<IRepository<Timesheet>>(), IoC.Resolve<ICacheService>());

                return _instance;
            }
        }

        private TimesheetService(IRepository<Timesheet> repository, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _repository = repository;
        }

        /// <summary>
        /// </summary>
        /// <param name="employerID"></param>
        /// <param name="weekStart">The day that the work week starts, usually monday.</param>
        /// <returns></returns>
        public WeeklyTimesheet GetWeeklyTimesheet(int employeeID, DateTime weekStart)
        {
            var parameters = new TimesheetParameters(new TimesheetDTO() { EmployeeID = employeeID, StartDate = weekStart.ToString() }, RepositoryAction.GetAll).Parameters;
            var timesheets = _repository.GetAll(parameters);

            var model = new WeeklyTimesheet();
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

        public IEnumerable<TimesheetDTO> GetAllWithinRange(TimesheetDTO dto)
        {
            var parameters = new TimesheetParameters(dto, RepositoryAction.GetAll).Parameters;
            return _repository.GetAll(parameters).Select(x => new TimesheetDTO(x)).ToArray();
        }

        public bool Create(TimesheetDTO dto)
        {
            var parameters = new TimesheetParameters(dto, RepositoryAction.Create).Parameters;
            try
            {
                _repository.Create(parameters);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}