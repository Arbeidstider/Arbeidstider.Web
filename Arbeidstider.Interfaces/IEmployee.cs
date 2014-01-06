namespace Arbeidstider.Interfaces
{
    public interface IEmployee : ITinyModel
    {
        string Firstname { get; set; }
        string Lastname { get; set; }
        string Fullname { get; }
        string Username { get; set; }
        string Mobile { get; set; }
        int UserId { get; set; }
        int WorkplaceId { get; set; }
    }
}
