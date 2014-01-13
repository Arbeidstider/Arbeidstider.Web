namespace Arbeidstider.Interfaces
{
    public interface IEmployee : ITinyModel
    {
        int WorkplaceId { get; set; }
        string Username { get; set; }
    }
}
