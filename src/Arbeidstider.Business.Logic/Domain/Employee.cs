using System;
using Arbeidstider.Business.Interfaces.Domain;
using Arbeidstider.Business.Logic.Enums;

namespace Arbeidstider.Business.Logic.Domain
{
    public class Employee : IEmployee
    {
        public int EmployeeID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Fullname { get { return Firstname + " " + Lastname; }}
        public string Username { get; set; }
        public string Mobile { get; set; }
        public Guid UserID { get; set; }
        public int EmployeeGroup { get; set; }
        public int WorkplaceID { get; set; }
    }
}