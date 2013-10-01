using System;
using System.Collections.Generic;
using System.Linq;
using Arbeidstider.Business.Interfaces.Caching;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.Caching;
using Arbeidstider.Business.Logic.Domain;
using Arbeidstider.Business.Logic.Enums;
using Arbeidstider.Business.Logic.Repository.Exceptions;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.Parameters;
using Arbeidstider.Web.Framework.ViewModels.Timesheet;
using log4net;

namespace Arbeidstider.Web.Framework.Services
{
    public class TimesheetService
    {
        private readonly ICacheService _cacheService; 
        private readonly IRepository<Timesheet> _repository;
        private static TimesheetService _instance;
        private readonly ILog Logger;

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
            Logger = IoC.Resolve<ILog>();
        }

        /// <summary>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="weekStart">The day that the work week starts, usually monday.</param>
        /// <returns></returns>
        public WeeklyTimesheet GetWeeklyTimesheet(Guid userID, DateTime weekStart)
        {
            try
            {
                var model = new WeeklyTimesheet();
                model.Shifts = _cacheService.Get(CacheKeys.GetWeeklyTimesheet, () => 
                        ParseTimesheetsToShifts(_repository.GetAll(new TimesheetParameters(new TimesheetDTO() {UserID = userID, StartDate = weekStart.ToString()},
                        RepositoryAction.GetAll).Parameters)));

                return model;
            }
            catch (TimesheetRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
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
            return _cacheService.Get(CacheKeys.GetAllWithinRange,
                () => _repository.GetAll(parameters).Select(x => new TimesheetDTO(x)).ToArray());
        }
        
        public bool Create(TimesheetDTO dto)
        {
            try
            {
                var parameters = new TimesheetParameters(dto, RepositoryAction.Create).Parameters;
                _repository.Create(parameters);
                return true;
            }
            catch (TimesheetRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }

        public TimesheetDTO Update(TimesheetDTO dto)
        {
            return new TimesheetDTO();
        }
    }
}