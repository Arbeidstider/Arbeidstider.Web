namespace Arbeidstider.Interfaces
{
    public interface IEmployeeSession
    {
        string Username { get; set; }
        int SessionId { get; set; }
        int UserId { get; set; }
    }
}
