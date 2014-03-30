using System;
using System.Collections.Generic;
using System.Linq;
using Arbeidstider.DataInterfaces;
using Arbeidstider.Repository;
using Arbeidstider.Repository.Exceptions;
using Arbeidstider.Repository.Parameters;
using Arbeidstider.DataObjects.DTO;
using Arbeidstider.Web.Framework.Cache;
using ServiceStack;

namespace Arbeidstider.DataServices
{
    public class TimesheetService : ServiceBase
    {
        private readonly TimesheetRepository _repository;
        private static TimesheetService _instance;
        private static TimeSpan _defaultExpiration = new TimeSpan(0, 15, 0);

        public static TimesheetService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TimesheetService(new TimesheetRepository());

                return _instance;
            }
        }

        private TimesheetService(TimesheetRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<TimesheetDTO> GetAllWithinRange(DateTime startDate, DateTime endDate, int? employeeId, int? workplaceId)
        {
            var parameters = TimesheetParameters.Create(employeeId: employeeId, startDate: startDate, endDate: endDate, workplaceId: workplaceId);
            var cacheKey = CacheKey.Create(CacheKeys.GetAllTimesheets, parameters);

            Func<IEnumerable<TimesheetDTO>> callback = () => _repository.GetAll(parameters).Select(x => new TimesheetDTO((ITimesheet)x)).ToList();

            return CacheClient.GetFromOrAddToCache<IEnumerable<TimesheetDTO>>(cacheKey, callback, _defaultExpiration);
        }

        public int CreateTimesheet(int employeeId, DateTime shiftDate, TimeSpan shiftStart, TimeSpan shiftEnd)
        {
            var parameters = TimesheetParameters.Create(employeeId: employeeId, shiftDate: shiftDate, shiftStart: shiftStart, shiftEnd: shiftEnd);
            try
            {
                return _repository.Create(parameters);
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

        public IEnumerable<ScheduleEventDTO> GetCurrentTimesheetWeek(int? employeeId = null, int? workplaceId = null)
        {
            var monday = GetMondayDate();
            var parameters = TimesheetParameters.Create(startDate: monday, endDate: monday.AddDays(6),
                                                        employeeId: employeeId, workplaceId: workplaceId);
            var timesheets = _repository.GetAll(parameters);
            var timesheetWeek = ScheduleService.GetCalendarWeek((IEnumerable<ITimesheet>)timesheets);

            return timesheetWeek;
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

        public TimesheetDTO GetTimesheet(int timesheetId)
        {
            var parameters = TimesheetParameters.Create(id: timesheetId);
            var timesheet = _repository.Get(parameters);
            return new TimesheetDTO((ITimesheet)timesheet);
        }

        public WorkingHoursDTO GetUpcomingWorkingHours(int employeeId)
        {
            var timesheets = TimesheetService.Instance.GetAllWithinRange(DateTime.Now.AddDays(1),
                                                                                           DateTime.Now.AddDays(14),
                                                                                           employeeId, null);

            return (from timesheet in timesheets
                    let shiftDate = DateTime.Parse(timesheet.ShiftDate)
                    where shiftDate > DateTime.Now.Date
                    select new WorkingHoursDTO(timesheet)).FirstOrDefault();
        }
    }
}