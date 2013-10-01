using System;
using System.Collections.Generic;
using Arbeidstider.Business.Logic.Enums;
using Arbeidstider.Web.Framework.DTO;

namespace Arbeidstider.Web.Framework.Parameters
{
    public class TimesheetParameters : IParameters
    {
        private readonly TimesheetDTO _timesheet = null;
        private readonly RepositoryAction _action;
        private readonly DateTime _selectedDay;
        private readonly TimeSpan _shiftStart;
        private readonly TimeSpan _shiftEnd;
        private readonly Guid _userID;


        public TimesheetParameters(TimesheetDTO timesheet, RepositoryAction action)
        {
            _action = action;
            _timesheet = timesheet;
            Create();
        }

        public TimesheetParameters(Guid userID, DateTime selectedDay, TimeSpan shiftStart, TimeSpan shiftEnd, RepositoryAction action)
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
                    var startDate = DateTime.Parse(_timesheet.StartDate).Date;
                    Parameters = new List<KeyValuePair<string, object>>()
                    {
                        new KeyValuePair<string, object>("@UserID", _timesheet.UserID),
                        new KeyValuePair<string, object>("@StartDate", startDate),
                        new KeyValuePair<string, object>("@EndDate", string.IsNullOrEmpty(_timesheet.EndDate) ? startDate.AddHours(8) : DateTime.Parse(_timesheet.EndDate).Date),
                    };
                    break;
                }
                case RepositoryAction.Create:
                {
                    if (_timesheet != null)
                    {
                        Parameters = new List<KeyValuePair<string, object>>()
                        {
                            new KeyValuePair<string, object>("@UserID", _timesheet.UserID),
                            new KeyValuePair<string, object>("@SelectedDay", _timesheet.SelectedDay),
                            new KeyValuePair<string, object>("@ShiftStart", _timesheet.ShiftStart),
                            new KeyValuePair<string, object>("@ShiftEnd",  _timesheet.ShiftEnd)
                        };
                    }
                    else
                    {
                        Parameters = new List<KeyValuePair<string, object>>()
                        {
                            new KeyValuePair<string, object>("@UserID", _userID),
                            new KeyValuePair<string, object>("@SelectedDay", _selectedDay),
                            new KeyValuePair<string, object>("@ShiftStart", _shiftStart),
                            new KeyValuePair<string, object>("@ShiftEnd", _shiftEnd)
                        };
                    }
                    break;
                }
            }
        }

        public List<KeyValuePair<string, object>> Parameters { get; set; }

        public void Validate()
        {
            switch (_action)
            {
                case RepositoryAction.GetAll:
                {
                    //if (DateTime.Parse(_timesheet.EndDate) < DateTime.Parse(_timesheet.StartDate)) throw new Exception("The end date must be a date happening after the start date.");
                    break;
                }
                case RepositoryAction.Create:
                {
                    //if (_timesheet.SelectedDay == string.Empty) throw new Exception("You need to specify selected day");
                    break;
                }
            }
        }

    }
}