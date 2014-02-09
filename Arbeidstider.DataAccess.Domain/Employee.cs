using Arbeidstider.Interfaces;

namespace Arbeidstider.DataAccess.Domain
{
    public class Employee : IEmployee
    {
        public int Id { get; set ;}
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Fullname { get { return Firstname + " " + Lastname; }}
        public string Username { get; set; }
        public string Mobile { get; set; }
        public int UserId { get; set; }
        public int EmployeeGroup { get; set; }
        public int WorkplaceId { get; set; }
        public bool IsTiny { get; set; }
    }
}