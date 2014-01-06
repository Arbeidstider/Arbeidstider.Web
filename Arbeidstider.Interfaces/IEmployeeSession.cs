namespace Arbeidstider.Web.Framework.Session
{
    public interface IEmployeeSession
    {
        string Username { get; set; }
        int SessionId { get; set; }
    }
}
