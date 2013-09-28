using System.Collections.Generic;
using System.Data;
using System.Linq;
using Arbeidstider.Business.Logic.Domain;

namespace Arbeidstider.Business.Logic.Factories
{
    internal class EmployeeFactory
    {
        internal static Employee Create(DataRow row)
        {
            var Employee = new Employee();
            Employee.EmployeeID = (int) row["EmployeeID"];
            Employee.Firstname = (string) row["Firstname"];
            Employee.Lastname = (string) row["Lastname"];
            Employee.Mobile = (string) row["Mobile"];
            Employee.Username = (string) row["Username"];

            return Employee;
        }

        public static IEnumerable<Employee> CreateArray(DataRowCollection rows)
        {
            return (from DataRow row in rows select Create(row)).ToList();
        }
    }
}
