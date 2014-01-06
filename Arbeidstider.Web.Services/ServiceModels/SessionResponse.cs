using Arbeidstider.Web.Framework.Session;
using ServiceStack;
using ServiceStack.Auth;

namespace Arbeidstider.Web.Services.ServiceModels
{
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