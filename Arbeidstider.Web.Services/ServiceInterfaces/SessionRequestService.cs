using Arbeidstider.Web.Framework.Session;
using Arbeidstider.Web.Services.App_Start;
using Arbeidstider.Web.Services.ServiceModels;
using ServiceStack;
using ServiceStack.Redis;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    public class SessionRequestService : Service
    {
        [EnableCors]
        public object Options(SessionRequest request)
        {
            return true;
        }

        [CustomAuthenticate("EmployeeAuth")]
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
                        if (Request.Headers["ss-pid"] != null && session.Id == Request.Headers["ss-pid"])
                        {
                            return new SessionRequestResponse() {AuthSession = session};
                        }
                        if (Request.Headers["ss-id"] != null && session.Id == Request.Headers["ss-id"])
                        {
                            return new SessionRequestResponse() {AuthSession = session};
                        }
                        if (Request.Headers["Session-Id"] != null && session.Id == Request.Headers["Session-Id"])
                        {
                            return new SessionRequestResponse() {AuthSession = session};
                        }
                    }
                }

                return new SessionRequestResponse() {};
            }
        }
    }
}