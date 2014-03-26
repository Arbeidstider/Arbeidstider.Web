
using Arbeidstider.Web.Framework.Session;
using Arbeidstider.Web.Services.App_Start;
using Arbeidstider.Web.Services.Attributes;
using Arbeidstider.Web.Services.ServiceModels;
using ServiceStack;
using ServiceStack.Redis;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    //[CustomAuthenticate("Employee")]
    //public class SessionService : Service
    //{
    //    //[Authenticate("Employee")]
    //    public object Get(GetSession request)
    //    {
    //        using (var redis = AppHost.Instance.Resolve<IRedisClientsManager>().GetClient())
    //        {
    //            var sessionkeys = redis.SearchKeys("urn:iauthsession:*");
    //            foreach (var key in sessionkeys)
    //            {
    //                var session = redis.Get<EmployeeSession>(key);
    //                if (session != null)
    //                {
    //                    if (session.SessionId == request.SessionId)
    //                    {
    //                        return new SessionResponse() { AuthSession = session, IsVerified =  true};
    //                    }
    //                }
    //            }
    //            return new SessionResponse() { AuthSession = null, IsVerified = false};
    //        }
    //    }

        // Verify session
        //public object Post(VerifySession request)
        //{
        //    var session = GetValidSession(request.SessionId);
        //    if (session != null)
        //    {
        //        return new SessionResponse() {IsVerified = true, AuthSession = session };
        //    }
        //        return new SessionResponse() {IsVerified = false};
        //}

        //private static EmployeeSession GetValidSession(string sessionId)
        //{
        //    var currentSession = this.GetSession();
        //    using (var redis = AppHost.Instance.Resolve<IRedisClientsManager>().GetClient())
        //    {
        //        var sessionkeys = redis.SearchKeys("urn:iauthsession:*");
        //        foreach (var key in sessionkeys)
        //        {
        //            var session = redis.Get<EmployeeSession>(key);
        //            if (session != null)
        //            {
        //                if (session.SessionId == sessionId)
        //                {
        //                    return session;
        //                }
        //            }
        //        }
        //        return null;
        //    }
        //}
}