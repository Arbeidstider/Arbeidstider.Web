using System;
using System.Collections.Generic;
using System.Linq;
using Arbeidstider.DataInterfaces;
using Arbeidstider.DataObjects.DTO;
using Arbeidstider.Repository;
using Dapper;
using ServiceStack;

namespace Arbeidstider.DataServices
{
    public class ScheduleService : ServiceBase
    //public class ScheduleService : DataServiceBase
    {
        public static ScheduleService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ScheduleService(new ScheduleRepository());

                return _instance;
            }
        }
        private readonly ScheduleRepository _repository;
        private static ScheduleService _instance;
        private ScheduleService(ScheduleRepository repository)
        {
            _repository = repository;
        }

        public List<ScheduleEventDTO> GetCalendar(DateTime from, DateTime to, int? employeeId, int? workPlaceId)
        {
            var response = new List<ScheduleEventDTO> {};
            var parameters = new DynamicParameters();

            parameters.Add("Start", from);
            parameters.Add("End", to);
            parameters.Add("EmployeeId", employeeId);
            parameters.Add("WorkplaceId", workPlaceId);
            var repo =  _repository.GetAll(parameters);

            return response;
        }

        private const int SUNDAY = 7;
        private const int WEEK_LENGTH = 7;

        public static IEnumerable<ScheduleEventDTO> GetCalendarWeek(IEnumerable<ITimesheet> timesheets)
        {
            var calendarWeek = new List<ScheduleEventDTO>();
            for (int i = (int)DayOfWeek.Monday; i < WEEK_LENGTH + 1; i++)
            {
                DayOfWeek day;
                // convert to .NET standard
                if (i == SUNDAY) day = DayOfWeek.Sunday;
                else day = (DayOfWeek)i;

                var calendarDay = GetShiftsByDay(day, timesheets);
                calendarWeek.Add(calendarDay);
            }

            return calendarWeek;
        }

        public static ScheduleEventDTO GetShiftsByDay(DayOfWeek dayOfWeek, IEnumerable<ITimesheet> results)
        {
            var shifts = (from x in results
                          where x.ShiftDate.DayOfWeek == dayOfWeek
                          select new ShiftDTO(x)).ToList();
            //TODO
            return new ScheduleEventDTO(null);
        }
    }
}