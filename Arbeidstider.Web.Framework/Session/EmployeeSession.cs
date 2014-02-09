using Arbeidstider.Interfaces;
using ServiceStack;

namespace Arbeidstider.Web.Framework.Session
{
    public class EmployeeSession : AuthUserSession, IEmployeeSession
    {
        public string Username { get; set; }
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public int WorkplaceId { get; set; }
    }
}
