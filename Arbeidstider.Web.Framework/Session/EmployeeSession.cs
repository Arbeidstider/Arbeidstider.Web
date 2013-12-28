namespace Arbeidstider.Web.Framework.Session
{
    public class EmployeeSession : ServiceStack.AuthUserSession, IEmployeeSession
    {
        public string Username { get; set; }
        public int SessionId { get; set; }
    }
}