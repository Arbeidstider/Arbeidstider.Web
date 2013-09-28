using System;
using System.Collections.Generic;
using Arbeidstider.Business.Logic.Enums;
using Arbeidstider.Web.Framework.DTO;

namespace Arbeidstider.Web.Framework.Parameters
{
    public class TimesheetParameters : IParameters
    {
        private readonly TimesheetDTO _timesheet;
        private readonly RepositoryAction _action;
        public TimesheetParameters(TimesheetDTO timesheet, RepositoryAction action)
        {
            _action = action;
            _timesheet = timesheet;
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
                        new KeyValuePair<string, object>("@EmployeeID", _timesheet.EmployeeID),
                        new KeyValuePair<string, object>("@StartDate", _timesheet.StartDate),
                        new KeyValuePair<string, object>("@EndDate", _timesheet.EndDate),
                    };
                    break;
                }
                case RepositoryAction.Create:
                {
                    Parameters = new List<KeyValuePair<string, object>>()
                    {
                        new KeyValuePair<string, object>("@EmployeeID", _timesheet.EmployeeID),
                        new KeyValuePair<string, object>("@SelectedDay", _timesheet.SelectedDay),
                        new KeyValuePair<string, object>("@ShiftStart", _timesheet.ShiftStart),
                        new KeyValuePair<string, object>("@ShiftEnd", _timesheet.ShiftEnd)
                    };
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
                    if (_timesheet.EmployeeID == 0) throw new Exception("You have not specified a EmployeeID.");
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