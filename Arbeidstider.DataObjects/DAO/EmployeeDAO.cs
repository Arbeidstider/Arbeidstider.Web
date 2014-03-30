using Arbeidstider.DataInterfaces;

namespace Arbeidstider.DataObjects.DAO
{
    public class EmployeeDAO : EntityBase, IEmployee
    {
        public override int Id { get; set ;}
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Mobile { get; set; }
        public int UserId { get; set; }
        public int EmployeeGroup { get; set; }
        public int WorkplaceId { get; set; }
        public bool IsTiny { get; set; }
    }
}