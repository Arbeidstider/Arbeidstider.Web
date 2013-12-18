﻿using System;
using System.Collections.Generic;
using System.Linq;
using Arbeidstider.Cache;
using Arbeidstider.DataAccess.Domain;
using Arbeidstider.DataAccess.Repository;
using Arbeidstider.DataAccess.Repository.Exceptions;
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
        public IEnumerable<IEmployeeShift> GetWeeklyShifts(Guid userID, DateTime weekStart)
        {
            if (userID == Guid.Empty || weekStart == new DateTime()) 
                throw new Exception("Null parameters were sent to GetWeeklyTimesheet");

            var parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("UserID", userID));
            parameters.Add(new KeyValuePair<string, object>("StartDate", weekStart));
            parameters.Add(new KeyValuePair<string, object>("EndDate", weekStart.AddDays(6)));

            try
            {
                return Cache.Get(CacheKeys.GetWeeklyTimesheet,
                                 () => _getWeeklyTimesheet(parameters));
            }
            catch (TimesheetRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        #region GetWeeklyTimesheet Callback
        private IEnumerable<IEmployeeShift> _getWeeklyTimesheet(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return (from x in _repository.GetAll(parameters)
                        select new EmployeeShift(x)).ToArray();
        }
        #endregion

        public List<TimesheetDTO> GetAllWithinRange(DateTime startDate, DateTime endDate, Guid userID)
        {
            var parameters = new List<KeyValuePair<string, object>>
                                 {
                                     new KeyValuePair<string, object>("StartDate", startDate),
                                     new KeyValuePair<string, object>("EndDate", endDate),
                                     new KeyValuePair<string, object>("UserID", userID)
                                 };
            return Cache.Get(CacheKeys.GetAllWithinRange,
                () => _getAllWithinRange(parameters));
        }

        #region GetAllWithinRange Callback
        private List<TimesheetDTO> _getAllWithinRange(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return _repository.GetAll(parameters).Select(x => new TimesheetDTO(x)).ToList();
        }
        #endregion

        public bool Create(Guid userID, DateTime selectedDay, TimeSpan shiftStart, TimeSpan shiftEnd)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("UserID", userID));
            parameters.Add(new KeyValuePair<string, object>("ShiftDate", selectedDay.Date));
            parameters.Add(new KeyValuePair<string, object>("ShiftStart", shiftStart.ToString()));
            parameters.Add(new KeyValuePair<string, object>("ShiftEnd", shiftEnd.ToString()));

            try
            {
                _repository.Create(parameters);
                return true;
            }
            catch (TimesheetRepositoryException ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }

        public bool UpdateTimesheet(int shiftID, Guid? userID, DateTime? selectedDate, TimeSpan? startShift, TimeSpan? endShift)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            /* Id of timesheet to update */
            parameters.Add(new KeyValuePair<string, object>("ShiftID", shiftID));

            /* Change user of shiftworker */
            if (userID != null)
                parameters.Add(new KeyValuePair<string, object>("UserID", userID.Value));
            else
            {
                parameters.Add(new KeyValuePair<string, object>("ShiftDate", selectedDate.Value.Date));
                parameters.Add(new KeyValuePair<string, object>("ShiftStart", startShift.Value.ToString()));
                parameters.Add(new KeyValuePair<string, object>("ShiftEnd", endShift.Value.ToString()));
            }

            try
            {
                if (_repository.Update(parameters))
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

        public List<TimesheetDTO> GetWorkplaceTimesheets(int? workplaceId, DateTime? startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int timesheetID)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("EmployeeShiftID", timesheetID));
            return _repository.Delete(parameters);
        }
    }
}