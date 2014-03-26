using Arbeidstider.Web.Framework.Session;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    [Route("/session/verify/{sessionId}", "POST")]
    [Route("/session/verify", "POST")]
    public class VerifySession : IReturn<SessionResponse>
    {
        public VerifySession(string sessionId)
        {
            this.SessionId = sessionId;
        }
        public string SessionId { get; set; }
    }

    public class SessionResponse
    {
        public EmployeeSession AuthSession { get; set; }
        public bool IsVerified { get; set; }
    }
}
