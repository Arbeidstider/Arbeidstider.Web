using System;
using System.Collections.Generic;
using Arbeidstider.Interfaces;

namespace Arbeidstider.DataAccess.Repository.Constants.StoredProcedures
{
    public class Parameters
    {
        public static EmployeeParameters Employee
        {
            get
            {
                return new EmployeeParameters();
            }
        }

        public static TimesheetParameters Timesheet
        {
            get
            {
                return new TimesheetParameters();
            }
        }
    }

    public class EmployeeParameters
    {
        public IEnumerable<KeyValuePair<string, object>> Create(int userId, int workplaceId)
        {
                var parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>("UserId", userId));
                parameters.Add(new KeyValuePair<string, object>("WorkplaceID", workplaceId));
                return parameters;
        }

        public IEnumerable<KeyValuePair<string, object>> Get(IEmployee employee)
        {
            if (employee.UserId != null) return Get((int)employee.UserId);

            return Get(employee.Username);
        }

        private static IEnumerable<KeyValuePair<string, object>> Get(int userId)
        {
            return new List<KeyValuePair<string, object>>()
                       {
                           new KeyValuePair<string, object>("UserID", userId),
                       };
        }

        private static IEnumerable<KeyValuePair<string, object>> Get(string username)
        {
            return new List<KeyValuePair<string, object>>()
                       {
                           new KeyValuePair<string, object>("Username", username),
                       };
        }
    }

    public class TimesheetParameters
    {
        
    }
}
