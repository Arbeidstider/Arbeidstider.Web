using System.Data;
using Arbeidstider.Business.Logic.Domain;

namespace Arbeidstider.Business.Factories
{
    internal class EmployeeFactory
    {
        internal static Employee Create(DataRow row)
        {
            var Employee = new Employee();
            Employee.EmployeeID = (int) row["EmployeeID"];
            Employee.Firstname = (string) row["Firstname"];
            Employee.Lastname = (string) row["Lastname"];
            Employee.Email = (string) row["Email"];
            Employee.Mobile = (string) row["Email"];
            Employee.Username = (string) row["Username"];

            return Employee;
        }
    }
}
