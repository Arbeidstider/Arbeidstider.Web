using ServiceStack;

namespace Arbeidstider.Web.Framework.Session
{
    public class EmployeeSession : AuthUserSession
    {
        public int EmployeeId { get; set; }
        public string SessionId { get; set; }
        public int WorkplaceId { get; set; }
    }
}
