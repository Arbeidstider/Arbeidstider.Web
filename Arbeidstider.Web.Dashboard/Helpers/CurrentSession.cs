using System.Web;
using ServiceStack;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace Arbeidstider.Web.Dashboard.Helpers
{
    public class CurrentSession
    {
        public static bool IsAuthenticated
        {
            get
            {
                var session = GetSession();
                return session != null && session.IsAuthenticated; 
            } 
        }

        public static AuthUserSession GetSession(int userId, int sessionId)
        {
            return new AuthUserSession();
            // Change to use redis
            /*
            var client = new JsonServiceClient("http://localhost:8181");
            var request = new Arbeidstider.Web.Services.ServiceModels.SessionRequest();
            var response = client.Get(request);
            return response.AuthSession;
             */
        }

        public static AuthUserSession GetSession()
        {
            return new AuthUserSession();
            // Change to use redis
            /*
            var client = new JsonServiceClient("http://localhost:8181");
            var request = new Arbeidstider.Web.Services.ServiceModels.SessionRequest();
            var response = client.Get(request);
            return response.AuthSession;
             */
        }
    }
}