namespace Arbeidstider.Web.Framework.Session
{
    public class EmployeeSession : ServiceStack.AuthUserSession, IEmployeeSession
    {
        // Connect to employee db
        public string Username { get; set; }
        public int SessionId { get; set; }
        public int UserId { get; set; }
    }
}