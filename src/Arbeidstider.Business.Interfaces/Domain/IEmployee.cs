namespace Arbeidstider.Business.Interfaces.Domain
{
    public interface IEmployee
    {
        int EmployeeID { get; set; }
        int WorkplaceID { get; set; }
        int EmployeeGroupID { get; set; }
        string Firstname { get; set; }
        string Lastname { get; set; }
        string Email { get; set; }
        string Mobile { get; set; }
        string Username { get; set; }
        string PasswordHash { get; set; }
    }
}
