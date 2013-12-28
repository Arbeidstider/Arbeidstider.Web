using Arbeidstider.Web.Services.App_Start;
using Arbeidstider.Web.Services.ServiceModels;
using ServiceStack;
using ServiceStack.Caching;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    [EnableCors()]
    public class SessionRequestService : IService
    {
        public object Options()
        {
            return Any();
        }
        public object Get()
        {
            return Any();
        }

        public object Any()
        {
            return new SessionRequestResponse()
                       {
                           AuthSession = AppHost.Instance.Resolve<ICacheClient>().SessionAs<AuthUserSession>()
                       };
        }
    }
}