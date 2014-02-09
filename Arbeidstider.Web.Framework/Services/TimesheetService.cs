using System;
using System.Collections.Generic;
using System.Linq;
using Arbeidstider.DataAccess.Domain;
using Arbeidstider.DataAccess.Repository.Exceptions;
using Arbeidstider.Interfaces;
using Arbeidstider.Web.Framework.Cache;
using Arbeidstider.Web.Framework.DTO;

namespace Arbeidstider.Web.Framework.Services
{
    public class TimesheetService : ServiceBase
    {
        private readonly IRepository<ITimesheet> _repository;
        private static TimesheetService _instance;

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
        public IEnumerable<IEmployeeShift> GetWeeklyShifts(int userId, DateTime weekStart)
        {
            if (userId == 0 || weekStart == new DateTime())
                throw new Exception("Null parameters were sent to GetWeeklyTimesheet");

            var parameters = new
                             {
                                 UserId = userId,
                                 StartDate = weekStart,
                                 EndDate = weekStart.AddDays(6)
                             };


            EmployeeShift[] shifts;
            try
            {
                using (var cache = Arbeidstider.Web.Framework.Cache.CacheClient.GetClient())
                {
                    var cacheKey = CacheKey.Create(
                        CacheKeys.GetWeeklyTimesheet,
                        parameters);

                    shifts = cache.Get<EmployeeShift[]>(cacheKey);
                    if (shifts != null) return shifts;
                    else
                    {
                        shifts = (from x in _repository.GetAll(parameters)
                                  select new EmployeeShift(x)).ToArray();
                        if (shifts.Length > 0)
                        {
                            cache.Add(cacheKey, shifts, DateTime.Now.AddMinutes(15));
                        }
                    }
                }
            }
            catch (TimesheetRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        public List<TimesheetDTO> GetAllWithinRange(DateTime startDate, DateTime endDate, int? userId, int? workplaceId)
        {
            var parameters = new
            {
                UserId = userId,
                StartDate = startDate,
                EndDate = endDate,
                WorkplaceId = workplaceId
            };
            List<TimesheetDTO> timesheets;

            using (var cache = Arbeidstider.Web.Framework.Cache.CacheClient.GetClient())
            {
                var cacheKey = CacheKey.Create(
                    CacheKeys.GetAllTimesheets,
                    parameters);

                timesheets = cache.Get<List<TimesheetDTO>>(cacheKey);
                if (timesheets != null) return timesheets;
                else
                {
                    timesheets = _repository.GetAll(parameters).Select(x => new TimesheetDTO(x)).ToList();
                    if (timesheets.Count > 0)
                    {
                        cache.Add(cacheKey, timesheets, DateTime.Now.AddMinutes(15));
                    }
                }
            }
            return timesheets;
        }

        public bool Create(int userId, DateTime selectedDay, TimeSpan shiftStart, TimeSpan shiftEnd)
        {
            try
            {
                _repository.Create(
                    new
                    {
                        UserId = userId,
                        ShiftDate = selectedDay,
                        ShiftStart = shiftStart,
                        ShiftEnd = shiftEnd
                    }
                );
                return true;
            }
            catch (TimesheetRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }

        public bool UpdateTimesheet(int timesheetId, int? userId, DateTime? selectedDate, TimeSpan? startShift, TimeSpan? endShift)
        {
            try
            {
                return _repository.Update(
                new
                {
                    Id = timesheetId,
                    UserId = userId,
                    ShiftDate = selectedDate,
                    ShiftStart = startShift,
                    ShiftEnd = endShift
                });
            }
            catch (TimesheetRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }

        public bool Delete(int timesheetId)
        {
            var parameters = Parameters.Timesheet.Delete((ITimesheet)(object)new { Id = timesheetId });
            ;
            return _repository.Delete(parameters);
        }
    }
}