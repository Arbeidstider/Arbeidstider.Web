using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Arbeidstider.Business.Logic.Domain;
using Arbeidstider.Business.Logic.Enums;
using Arbeidstider.Business.Logic.Repository.Exceptions;

namespace Arbeidstider.Business.Logic.Factories
{
    internal class EmployeeFactory
    {
        internal static Employee Create(DataRow row)
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
                Employee.EmployeeGroup = (EmployeeGroup)(int) row["EmployeeGroupID"];

                return Employee;
            }
            catch (Exception ex)
            {
               throw new EmployeeRepositoryException(string.Format("Factory failed to create employee, exception: {0}", ex.Message)); 
            }
        }

        internal static IEnumerable<Employee> CreateArray(DataRowCollection rows)
        {
            return (from DataRow row in rows select Create(row)).ToArray();
        }
    }
}
