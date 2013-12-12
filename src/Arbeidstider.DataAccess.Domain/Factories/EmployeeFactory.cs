using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Arbeidstider.DataAccess.Domain.Factories
{
    public class EmployeeFactory
    {
        public static Employee Create(DataRow row)
        {
            try
            {
                var Employee = new Employee();
                Employee.Firstname = (string) row["Firstname"];
                Employee.Lastname = (string) row["Lastname"];
                Employee.Mobile = (string) row["Mobile"];
                Employee.Username = (string) row["Username"];
                Employee.UserID = Guid.Parse(row["UserID"].ToString());
                Employee.WorkplaceID = (int) row["WorkplaceID"];
                Employee.EmployeeGroup = (int) row["EmployeeGroupID"];

                return Employee;
            }
            catch (Exception ex)
            {
               throw new Exception(string.Format("Factory failed to create employee, exception: {0}", ex.Message)); 
            }
        }

        public static IEnumerable<Employee> CreateArray(DataRowCollection rows)
        {
            return (from DataRow row in rows select Create(row)).ToArray();
        }
    }
}