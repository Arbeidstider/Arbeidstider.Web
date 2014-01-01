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
        public IEnumerable<KeyValuePair<string, object>> Create(string username, Guid userID, string lastname, string firstname, string mobile, string birthDate, int workplaceID)
        {
                var parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>("UserID", userID));
                parameters.Add(new KeyValuePair<string, object>("Username", username));
                parameters.Add(new KeyValuePair<string, object>("Lastname", lastname));
                parameters.Add(new KeyValuePair<string, object>("Firstname", firstname));
                parameters.Add(new KeyValuePair<string, object>("Mobile", mobile));
                parameters.Add(new KeyValuePair<string, object>("Birthdate", birthDate));
                parameters.Add(new KeyValuePair<string, object>("EmployeeGroupID", 3));
                parameters.Add(new KeyValuePair<string, object>("WorkplaceID", workplaceID));
                return parameters;
        }

        public IEnumerable<KeyValuePair<string, object>> Get(IEmployee employee)
        {
            if (employee.UserID != null) return Get((Guid)employee.UserID);

            return Get(employee.Username);
        }

        private static IEnumerable<KeyValuePair<string, object>> Get(Guid userID)
        {
            return new List<KeyValuePair<string, object>>()
                       {
                           new KeyValuePair<string, object>("UserID", userID),
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
