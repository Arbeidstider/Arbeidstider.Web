using System;
using System.Collections.Generic;
using System.Linq;
using Arbeidstider.Business.Interfaces.Domain;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.Caching;
using Arbeidstider.Business.Logic.Repository.Exceptions;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.ViewModels.Timesheet;

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
        /// <param name="username"></param>
        /// <param name="weekStart">The day that the work week starts, usually monday.</param>
        /// <returns></returns>
        public IEnumerable<IEmployeeShift> GetWeeklyTimesheet(Guid userID, DateTime weekStart)
        {
            try
            {
                return Cache.Get(CacheKeys.GetWeeklyTimesheet, () =>
                    (from x in
                        _repository.GetAll(TimesheetDTO.Create(userID: userID, startDate: weekStart).Parameters())
                        select new EmployeeShift(x)).ToArray());
            }
            catch (TimesheetRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        public TimesheetModel GetTimesheet(Guid userID, DateTime selectedDay)
        {
            try
            {
                return Cache.Get(CacheKeys.GetTimesheet,
                    () => new TimesheetModel(_repository.Get(TimesheetDTO.Create(userID: userID, selectedDay: selectedDay).Parameters())));
            }
            catch (TimesheetRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        public IEnumerable<TimesheetModel> GetAllWithinRange(TimesheetDTO dto)
        {
            return Cache.Get(CacheKeys.GetAllWithinRange,
                () => _repository.GetAll(dto.Parameters()).Select(x => new TimesheetModel(x)).ToArray());
        }
        
        public bool Create(TimesheetDTO dto)
        {
            try
            {
                _repository.Create(dto.Parameters());
                return true;
            }
            catch (TimesheetRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }

        public bool UpdateTimesheet(TimesheetDTO updatedDTO)
        {
            try
            {
                _repository.Update(updatedDTO.Parameters());
                InvalidateTimesheetCache();
                return true;
            }
            catch (TimesheetRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }

        private static void InvalidateTimesheetCache()
        {
            Cache.Invalidate(CacheKeys.GetTimesheet);
            Cache.Invalidate(CacheKeys.GetWeeklyTimesheet);
            Cache.Invalidate(CacheKeys.GetAllWithinRange);
        }
    }
}