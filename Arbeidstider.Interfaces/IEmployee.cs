namespace Arbeidstider.Interfaces
{
    public interface IEmployee : ITinyModel
    {
        int Id { get; set; }
        int UserId { get; set; }
        int WorkplaceId { get; set; }
        string Username { get; set; }
        string DisplayName { get; set; }
        string PhoneNumber { get; set; }
    }
}