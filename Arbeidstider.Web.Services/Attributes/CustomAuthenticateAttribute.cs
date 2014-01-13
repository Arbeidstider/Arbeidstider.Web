using System;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Host;
using ServiceStack.Web;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CustomAuthenticateAttribute : RequestFilterAttribute
    {
        public string Provider { get; set; }
        public string HtmlRedirect { get; set; }
     
        public CustomAuthenticateAttribute(ApplyTo applyTo)
            : base(applyTo)
        {
            this.Priority = (int)RequestFilterPriority.Authenticate;
        }
     
        public CustomAuthenticateAttribute()
            : this(ApplyTo.All) { }
     
        public CustomAuthenticateAttribute(string provider)
            : this(ApplyTo.All)
        {
            this.Provider = provider;
        }

        public CustomAuthenticateAttribute(ApplyTo applyTo, string provider)
            : this(applyTo)
        {
            this.Provider = provider;
        }
     
        public override void Execute(IRequest req, IResponse res, object requestDto)
        {
            if (AuthenticateService.AuthProviders == null) throw new InvalidOperationException("The AuthService must be initialized by calling "
                                                                                       + "AuthService.Init to use an authenticate attribute");
     
            AuthenticateIfDigestAuth(req, res);
            AuthenticateIfBasicAuth(req, res);
            SetSessionIfSessionIdHeader(req, res);
        }
        
        private string sessionIdFromHeader(IRequest httpReq)
        {
            var sessionId = httpReq.Headers["Session-Id"].ToNullIfEmpty() ?? httpReq.Headers["ss-id"].ToNullIfEmpty() ?? httpReq.Headers["ss-pid"];
            return sessionId;
        }

        private void SetSessionIfSessionIdHeader(IRequest req, IResponse res)
        {
            var tokenSessionId = sessionIdFromHeader(req);
            if (tokenSessionId != null)
            {
                req.Items[SessionFeature.SessionId] = tokenSessionId;
                req.Items[SessionFeature.PermanentSessionId] = tokenSessionId;
            }
        }
     
        public static void AuthenticateIfBasicAuth(IRequest req, IResponse res)
        {
            //Need to run SessionFeature filter since its not executed before this attribute (Priority -100)    		
            SessionFeature.AddSessionIdToRequestFilter(req, res, null); //Required to get req.GetSessionId()
     
            var userPass = req.GetBasicAuthUserAndPassword();
            if (userPass != null)
            {
                var authService = req.TryResolve<AuthenticateService>();
                authService.Request = req;
                var response = authService.Post(new Authenticate()
                                                    {
                                                        provider = "credentials", // changed from AuthenticateAttribute, was "basic"
                                                        UserName = userPass.Value.Key,
                                                        Password = userPass.Value.Value
                                                    });
            }
     
        }

        public static void AuthenticateIfDigestAuth(IRequest req, IResponse res)
        {
            AuthenticateAttribute.AuthenticateIfDigestAuth(req, res);
        }
    }
}