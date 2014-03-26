using Arbeidstider.Web.Framework.Services;
using Arbeidstider.Web.Framework.Session;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Host;
using ServiceStack.Web;

namespace Arbeidstider.Web.Framework.AuthProviders
{
    public class EmployeeAuthProvider : BasicAuthProvider
    {
        public new static string Name = "Employee";
        /*
        public override object Authenticate(IServiceBase authService, IAuthSession session, Authenticate request)
        {
            var sessionId = session.Id;
            var httpReq = authService.Request;
            var basicAuth = httpReq.GetBasicAuthUserAndPassword();
            if (basicAuth == null)
                throw HttpError.Unauthorized("Invalid BasicAuth credentials");

            var userName = basicAuth.Value.Key;
            var password = basicAuth.Value.Value;

            if (!LoginMatchesSession(session, userName))
            {
                authService.RemoveSession();
                session = authService.GetSession();
            }

            if (TryAuthenticate(authService, userName, password))
            {
                if (session.UserAuthName == null)
                {
                    session.UserAuthName = userName;
                }

                var userSession = session as AuthUserSession;
                if (userSession != null)
                {
                    LoadUserAuthInfo(userSession, null, null);
                }

                var authRepo = authService.TryResolve<IAuthRepository>();
                if (authRepo != null)
                {
                    foreach (var oAuthToken in session.ProviderOAuthAccess)
                    {
                        var authProvider = AuthenticateService.GetAuthProvider(oAuthToken.Provider);
                        if (authProvider == null)
                        {
                            continue;
                        }
                        var userAuthProvider = authProvider as OAuthProvider;
                        if (userAuthProvider != null)
                        {
                            userAuthProvider.LoadUserOAuthProvider(session, oAuthToken);
                        }
                    }

                    var httpRes = authService.Request.Response as IHttpResponse;
                    if (httpRes != null)
                    {
                        httpRes.Cookies.AddPermanentCookie(HttpHeaders.XUserAuthId, session.UserAuthId);
                    }
                }

                session.Id = sessionId;
                session = session as EmployeeSession;
                authService.SaveSession(session, SessionExpiry);
                base.OnAuthenticated(authService, session, null, null);

                return new Authenticate
                {
                    UserName = userName,
                };
            }

            throw HttpError.Unauthorized("Invalid UserName or Password");
        }
         */

        public override object Authenticate(IServiceBase authService, IAuthSession session, Authenticate request)
        {
            var sessionId = session.Id;
            var httpReq = authService.Request;
            var basicAuth = httpReq.GetBasicAuthUserAndPassword();
            if (basicAuth == null)
                throw HttpError.Unauthorized("Invalid BasicAuth credentials");

            var userName = basicAuth.Value.Key;
            var password = basicAuth.Value.Value;

            if (!LoginMatchesSession(session, userName))
            {
                authService.RemoveSession();
                session = authService.GetSession();
            }

            if (TryAuthenticate(authService, userName, password))
            {
                if (session.UserAuthName == null)
                {
                    session.UserAuthName = userName;
                }

                var userSession = session as EmployeeSession;
                if (userSession != null)
                {
                    LoadUserAuthInfo(userSession, null, null);
                }

                var httpRes = authService.Request.Response as IHttpResponse;
                if (httpRes != null)
                {
                    httpRes.Cookies.AddPermanentCookie(HttpHeaders.XUserAuthId, session.UserAuthId);
                }

                var employee = EmployeeService.Instance.GetEmployee(session.UserName);

                // set employeemetadata on session
                userSession.SessionId = sessionId;
                userSession.Id = sessionId;
                userSession.EmployeeId = employee.Id;
                userSession.WorkplaceId = employee.WorkplaceId;

                authService.SaveSession(userSession, SessionExpiry);
                userSession.SessionId = sessionId;
                base.OnAuthenticated(authService, userSession, null, null);
                userSession.SessionId = sessionId;

                return new
                {
                    UserName = userName,
                    SessionId = userSession.SessionId,
                    EmployeeId = employee.Id,
                    UserId = userSession.UserAuthId,
                    WorkplaceId = employee.WorkplaceId
                };
            }
            throw HttpError.Unauthorized("Invalid UserName or Password");
        }
    }
}