using System;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    public class SessionRequest : IReturn<SessionRequestResponse>
    {
        public Guid UserId { get; set; }

        public string SessionId { get; set; }
    }
}