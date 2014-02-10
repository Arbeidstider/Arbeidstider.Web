using Arbeidstider.Web.Framework.Session;
using Arbeidstider.Web.Services.App_Start;
using Arbeidstider.Web.Services.ServiceModels;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Redis;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    public class SessionRequestService : Service
    {
        [CustomAuthenticate("Employee")]
        //[Authenticate("Employee")]
        public object Get(SessionRequest request)
        {
            using (var redis = AppHost.Instance.Resolve<IRedisClientsManager>().GetClient())
            {
                var sessionkeys = redis.SearchKeys("urn:iauthsession:*");
                foreach (var key in sessionkeys)
                {
                    var session = redis.Get<EmployeeSession>(key);
                    if (session != null)
                    {
                        if (session.SessionId == request.SessionId)
                        {
                            return new SessionRequestResponse() {AuthSession = session};
                        }
                    }
                }
            }
            return new SessionRequestResponse() {AuthSession = null};
        }
    }
}