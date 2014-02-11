using System;
using System.Collections.Generic;
using System.Linq;
using Arbeidstider.DataAccess.Domain;
using Arbeidstider.DataAccess.Repository.Exceptions;
using Arbeidstider.DataAccess.Repository.Parameters;
using Arbeidstider.Interfaces;
using Arbeidstider.Web.Framework.Cache;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.Models;

namespace Arbeidstider.Web.Framework.Services
{
    public class TimesheetService : ServiceBase
    {
        private readonly IRepository<ITimesheet> _repository;
        private static TimesheetService _instance;
        private static TimeSpan _defaultExpiration = new TimeSpan(0, 15, 0);

        public static TimesheetService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TimesheetService(IoC.Resolve<IRepository<ITimesheet>>());

                return _instance;
            }
        }

        private TimesheetService(IRepository<ITimesheet> repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IEmployeeShift> GetWeeklyShifts(int employeeId, DateTime weekStart)
        {
            if (employeeId == 0 || weekStart == new DateTime())
                throw new Exception("Null parameters were sent to GetWeeklyTimesheet");

            var parameters = TimesheetParameters.Create(employeeId: employeeId, startDate: weekStart, endDate: weekStart.AddDays(6));

            try
            {
                // CacheKey, Callback
                var cacheKey = CacheKey.Create(CacheKeys.GetWeeklyTimesheet, parameters);
                Func<IEnumerable<IEmployeeShift>> callback = () => (from x in _repository.GetAll(parameters)
                                                                    select new EmployeeShift(x))
                                                                       .ToArray();

                return CacheClient.GetFromOrAddToCache<IEnumerable<IEmployeeShift>>(cacheKey, callback, _defaultExpiration);
            }
            catch (TimesheetRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        public IEnumerable<TimesheetDTO> GetAllWithinRange(DateTime startDate, DateTime endDate, int? employeeId, int? workplaceId)
        {
            var parameters = TimesheetParameters.Create(employeeId: employeeId, startDate: startDate, endDate: endDate, workplaceId: workplaceId);
            var cacheKey = CacheKey.Create(CacheKeys.GetAllTimesheets, parameters);

            Func<IEnumerable<TimesheetDTO>> callback = () => _repository.GetAll(parameters).Select(x => new TimesheetDTO(x)).ToList();

            return CacheClient.GetFromOrAddToCache<IEnumerable<TimesheetDTO>>(cacheKey, callback, _defaultExpiration);
        }

        public int CreateTimesheet(int employeeId, DateTime shiftDate, TimeSpan shiftStart, TimeSpan shiftEnd)
        {
            var parameters = TimesheetParameters.Create(employeeId: employeeId, shiftDate: shiftDate, shiftStart:shiftStart, shiftEnd:shiftEnd);
            try
            {
                return  _repository.Create(parameters);
            }
            catch (TimesheetRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return 0;
            }
        }

        public bool UpdateTimesheet(int timesheetId, int? employeeId, DateTime? shiftDate, TimeSpan? startShift, TimeSpan? endShift)
        {
            try
            {
                var parameters = TimesheetParameters.Create(id: timesheetId, employeeId: employeeId ?? 0,
                                                            shiftDate: shiftDate ?? DateTime.MinValue, shiftStart: startShift ?? TimeSpan.Zero,
                                                            shiftEnd: endShift ?? TimeSpan.Zero);
                return _repository.Update(parameters);
            }
            catch (TimesheetRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }

        public bool Delete(int timesheetId)
        {
            var parameters = TimesheetParameters.Create(id: timesheetId);
            ;
            return _repository.Delete(parameters);
        }

        public TimesheetCalendar GetCurrentTimesheetWeek(int? employeeId = null, int? workplaceId = null)
        {
            var monday = GetMondayDate();
            var parameters = TimesheetParameters.Create(startDate: monday, endDate: monday.AddDays(6),
                                                        employeeId: employeeId, workplaceId: workplaceId);
            var results = _repository.GetAll(parameters);
            var weeklyCalendar = new TimesheetCalendar(results);

            return weeklyCalendar;
        }

        private static DateTime GetMondayDate(DateTime? dayOfWeek = null)
        {
            if (dayOfWeek == null)
            {
                return DateTime.Now.DayOfWeek != DayOfWeek.Monday ? FindMondayDate(DateTime.Now) : DateTime.Now;
            }

            return dayOfWeek.Value.DayOfWeek == DayOfWeek.Monday ? dayOfWeek.Value : FindMondayDate(dayOfWeek.Value);
        }

        private static DateTime FindMondayDate(DateTime date)
        {
            while (date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.AddDays(-1);
                if (date.DayOfWeek == DayOfWeek.Monday) break;
            }

            return date;
        }

        private static IEnumerable<TimesheetDTO> GetTimesheetsByDay(IEnumerable<TimesheetDTO> results, int i)
        {
            return (from x in results
                    where !string.IsNullOrEmpty(x.ShiftDate)
                       && (((int)(DateTime.Parse(x.ShiftDate).DayOfWeek)) == i)
                    select x).ToArray();

        }
    }
}