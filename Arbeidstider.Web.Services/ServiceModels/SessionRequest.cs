using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    public class SessionRequest : IReturn<SessionRequestResponse>
    {
        public string SessionId { get; set; }
    }
}