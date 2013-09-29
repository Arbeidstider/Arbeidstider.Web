using System;
using Arbeidstider.Business.Logic.Enums;

namespace Arbeidstider.Business.Logic.Domain
{
    public class Employee
    {
        public Guid UserID { get; set; }
        public int EmployeeID { get; set; }
        public int WorkplaceID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public EmployeeGroup EmployeeGroup { get; set; }
        public string Fullname { get { return Firstname + " " + Lastname; }}
        public Workplace Workplace { get; set; }
        public string Username { get; set; }
        public string Passwordhash { get; set; }
        public bool RememberMe { get; set; }
        public string Mobile { get; set; }
    }
}