using System;

namespace Arbeidstider.DataAccess.Domain
{
    public interface IEmployee
    {
        string Firstname { get; set; }
        string Lastname { get; set; }
        string Fullname { get; }
        string Username { get; set; }
        string Mobile { get; set; }
        Guid UserID { get; set; }
        int EmployeeGroup { get; set; }
        int WorkplaceID { get; set; }
    }
}
