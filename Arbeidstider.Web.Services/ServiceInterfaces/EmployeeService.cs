using System.Text;
using Arbeidstider.Web.Framework.Session;
using Arbeidstider.Web.Services.App_Start;
using Arbeidstider.Web.Services.Attributes;
using Arbeidstider.Web.Services.ServiceInterfaces;
using Arbeidstider.Web.Services.ServiceModels;
using ServiceStack;
using ServiceStack.Redis;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    [CustomAuthenticate("Employee")]
    public class EmployeeService : Service
    {
        public EmployeeSession Get(GetEmployeeSession request)
        {
            var sessionId = this.GetSessionId();
            StringBuilder stuff = new StringBuilder();
            using (var redis = AppHost.Instance.Resolve<IRedisClientsManager>().GetClient())
            {
                var sessionkeys = redis.SearchKeys("urn:iauthsession:*");
                foreach (var key in sessionkeys)
                {
                    var session = redis.Get<EmployeeSession>(key);
                    if (session != null)
                    {
                        stuff.AppendLine("redis session: ");
                        stuff.AppendLine(sessionId);
                        stuff.AppendLine(session.SessionId);
                        stuff.AppendLine("" + session.EmployeeId);
                        stuff.AppendLine("-----------------");
                        if (session.SessionId == sessionId && session.EmployeeId == request.EmployeeId)
                        {
                            session.IsAuthenticated = true;
                            return session;
                        }
                    }
                }
                return new EmployeeSession() {Stuff = stuff.ToString()};
            }
            return null;
        }
    }
}