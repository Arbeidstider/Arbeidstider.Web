using System;
using System.Collections.Generic;
using Arbeidstider.Business.Logic.Enums;
using Arbeidstider.Web.Framework.DTO;

namespace Arbeidstider.Web.Framework.Parameters
{
    public class TimesheetParameters : ParameterBase
    {
        private readonly TimesheetDTO _dto = null;
        private readonly RepositoryAction _action;
        private readonly DateTime _selectedDay;
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;
        private readonly TimeSpan _shiftStart;
        private readonly TimeSpan _shiftEnd;
        private readonly Guid _userID;

        public TimesheetParameters(TimesheetDTO dto, RepositoryAction action) : base()
        {
            _action = action;
            if (dto.UserID != Guid.Empty) _userID = dto.UserID;
            if (!string.IsNullOrEmpty(dto.StartDate)) _startDate = DateTime.Parse(dto.StartDate).Date;
            if (!string.IsNullOrEmpty(dto.EndDate)) _endDate = DateTime.Parse(dto.EndDate).Date;
            if (!string.IsNullOrEmpty(dto.SelectedDay)) _selectedDay = DateTime.Parse(dto.SelectedDay).Date;
            if (!string.IsNullOrEmpty(dto.ShiftStart)) _shiftStart = TimeSpan.Parse(dto.ShiftStart);
            if (!string.IsNullOrEmpty(dto.ShiftEnd)) _shiftEnd = TimeSpan.Parse(dto.ShiftEnd);
            Create();
        }

        public TimesheetParameters(Guid userID, DateTime weekStart, RepositoryAction action) : base()
        {
            _action = action;
            _userID = userID;
            _startDate = weekStart;
            _endDate = weekStart.AddDays(6);
            Create();
        }

        public TimesheetParameters(Guid userID, DateTime selectedDay, TimeSpan shiftStart, TimeSpan shiftEnd, RepositoryAction action) : base()
        {
            _userID = userID;
            _selectedDay = selectedDay;
            _shiftStart = shiftStart;
            _shiftEnd = shiftEnd;
            _action = action;
            Create();
        }

        public void Create()
        {
            switch (_action)
            {
                case RepositoryAction.GetAll:
                {
                    Parameters = new List<KeyValuePair<string, object>>()
                    {
                        new KeyValuePair<string, object>("@UserID", _userID),
                        new KeyValuePair<string, object>("@StartDate", _startDate),
                        new KeyValuePair<string, object>("@EndDate", _endDate),
                    };
                    break;
                }
                case RepositoryAction.Create:
                {
                    Parameters = new List<KeyValuePair<string, object>>()
                    {
                        new KeyValuePair<string, object>("@UserID", _userID),
                        new KeyValuePair<string, object>("@SelectedDay", _selectedDay.Date),
                        new KeyValuePair<string, object>("@ShiftStart", _shiftStart),
                        new KeyValuePair<string, object>("@ShiftEnd", _shiftEnd)
                    };
                    break;
                }
            }
        }

        public List<KeyValuePair<string, object>> Parameters { get; set; }
    }
}