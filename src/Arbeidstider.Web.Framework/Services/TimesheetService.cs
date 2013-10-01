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

namespace Arbeidstider.Web.Framework.Services
{
    public class TimesheetService : ServiceBase
    {
        private readonly IRepository<Timesheet> _repository;
        private static TimesheetService _instance;

        public static TimesheetService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TimesheetService(IoC.Resolve<IRepository<Timesheet>>());

                return _instance;
            }
        }

        private TimesheetService(IRepository<Timesheet> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="weekStart">The day that the work week starts, usually monday.</param>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<DateTime, EmployeeShift>> GetWeeklyTimesheet(Guid userID, DateTime weekStart)
        {
            try
            {
                return Cache.Get(CacheKeys.GetWeeklyTimesheet, () => 
                        ParseTimesheetsToShifts(_repository.GetAll(new TimesheetParameters(userID, weekStart,
                        RepositoryAction.GetAll).Parameters)));
            }
            catch (TimesheetRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        public IEnumerable<TimesheetDTO> GetAllWithinRange(TimesheetDTO dto)
        {
            var parameters = new TimesheetParameters(dto, RepositoryAction.GetAll).Parameters;
            return Cache.Get(CacheKeys.GetAllWithinRange,
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

        #region Private Methods
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
        #endregion
    }
}