using System.Collections.Generic;
using Arbeidstider.ServiceInterfaces;
using Arbeidstider.ServiceModels;
using ServiceStack;
using ServiceStack.Auth;

namespace Arbeidstider.AppHost.AuthProviders
{
    public class BrowserAuthProvider : BasicAuthProvider
    {
        public new static string Name = "Browser";
        public new static string Provider = "Browser";

       public override void OnAuthenticated(IServiceBase authService, IAuthSession session, IAuthTokens tokens, Dictionary<string, string> authInfo)
       {
           var userSession = session as EmployeeSession;
            if (userSession != null)
            {
                LoadUserAuthInfo(userSession, tokens, authInfo);
            }

            var authRepo = authService.TryResolve<IUserAuthRepository>();
            if (authRepo != null)
            {
                if (tokens != null)
                {
                    authInfo.ForEach((x, y) => tokens.Items[x] = y);
                    session.UserAuthId = authRepo.CreateOrMergeAuthSession(session, tokens);
                }
                
                //foreach (var oAuthToken in session.ProviderOAuthAccess)
                //{
                //    var authProvider = AuthService.GetAuthProvider(oAuthToken.Provider);
                //    if (authProvider == null) continue;
                //    var userAuthProvider = authProvider as OAuthProvider;
                //    if (userAuthProvider != null)
                //    {
                //        userAuthProvider.LoadUserOAuthProvider(session, oAuthToken);
                //    }
                //}

                var httpRes = authService.Request.Response;
                if (httpRes != null)
                {
                    httpRes.SetPermanentCookie(HttpHeaders.XUserAuthId, session.UserAuthId);
                }
                
            }

           var employee = Arbeidstider.DataServices.EmployeeService.Instance.GetEmployee(session.UserName);
           if (employee != null)
           {
               userSession.EmployeeId = employee.id;
               userSession.WorkplaceId = employee.workplaceId;
           }

           userSession.UserName = session.UserName;
            authService.SaveSession(userSession, SessionExpiry);
            userSession.OnAuthenticated(authService, userSession, tokens, authInfo);
        }

        public override object Authenticate(IServiceBase authService, IAuthSession session, Authenticate request)
        {
            string userName = request.UserName;
            string password = request.Password;
            if (!LoginMatchesSession(session, userName))
            {
                authService.RemoveSession();
                session = authService.GetSession();
            }

            if (base.TryAuthenticate(authService, userName, password))
            {
                if (session.UserAuthName == null)
                    session.UserAuthName = userName;
                
                this.OnAuthenticated(authService, session, null, null);
                var userSession = session as EmployeeSession;

                return new  {
                    userName = userName,
                    SessionId = session.Id,
                    employeeId = userSession.EmployeeId,
                    workplaceId = userSession.WorkplaceId,
                    roles = userSession.Roles,
                    displayName = userSession.DisplayName
                };
            }

            throw HttpError.Unauthorized("Invalid UserName or Password");
        }
    }
}