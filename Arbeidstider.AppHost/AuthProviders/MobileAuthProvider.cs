using ServiceStack;
using ServiceStack.Auth;

namespace Arbeidstider.AppHost.AuthProviders
{
    public class MobileAuthProvider : IAuthProvider
    {
        public new static string Name = "Mobile";
        public object Logout(IServiceBase service, Authenticate request)
        {
            throw new System.NotImplementedException();
        }

        public object Authenticate(IServiceBase authService, IAuthSession session, Authenticate request)
        {
            throw new System.NotImplementedException();
        }

        public bool IsAuthorized(IAuthSession session, IAuthTokens tokens, Authenticate request = null)
        {
            throw new System.NotImplementedException();
        }

        public string AuthRealm { get; set; }
        public string Provider { get; set; }
        public string CallbackUrl { get; set; }
    }
}