using System;
using Arbeidstider.Interfaces;

namespace Arbeidstider.DataAccess.Domain
{
    public class Employee : IEmployee
    {
        public Employee()
        {
            
        }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Fullname { get { return Firstname + " " + Lastname; }}
        public string Username { get; set; }
        public string Mobile { get; set; }
        public Guid? UserID { get; set; }
        public int EmployeeGroup { get; set; }
        public int WorkplaceID { get; set; }
    }
}