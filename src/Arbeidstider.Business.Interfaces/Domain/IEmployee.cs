using System;

namespace Arbeidstider.Business.Interfaces.Domain
{
    public interface IEmployee
    {
        int EmployeeID { get; set; }
        Guid UserID { get; set; }
        int WorkplaceID { get; set; }
        int EmployeeGroupID { get; set; }
        string Mobile { get; set; }
        string Username { get; set; }
    }
}
