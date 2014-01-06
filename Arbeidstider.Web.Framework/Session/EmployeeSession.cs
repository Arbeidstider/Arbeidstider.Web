using System.Collections.Generic;
using ServiceStack;
using ServiceStack.Auth;

namespace Arbeidstider.Web.Framework.Session
{
    public class EmployeeSession : ServiceStack.AuthUserSession
    {
        public int WorkplaceId { get; set; }
        // Connect to employee db
        public override void OnAuthenticated(IServiceBase authService, IAuthSession session, IAuthTokens tokens,
                                             Dictionary<string, string> authInfo)
        {

            base.OnAuthenticated(authService, session, tokens, authInfo);
        }
    }
}