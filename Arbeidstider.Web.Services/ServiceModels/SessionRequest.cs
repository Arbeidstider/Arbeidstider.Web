using Arbeidstider.Web.Framework.Session;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    public class SessionRequest : IReturn<SessionRequestResponse>
    {
        public string SessionId { get; set; }
    }

    public class SessionRequestResponse : IHasResponseStatus
    {
        public SessionRequestResponse()
        {
            this.ResponseStatus = new ResponseStatus();
        }
        public EmployeeSession AuthSession { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}