﻿using System.Configuration;
using System.Text;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.Session;
using Arbeidstider.Web.Services.App_Start;
using Arbeidstider.Web.Services.Attributes;
using Arbeidstider.Web.Services.ServiceModels;
using NLog;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Logging;
using ServiceStack.Redis;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    //[CustomAuthenticate("Employee")]
    public class EmployeeService : ServiceBase
    {
        public ILog Logger { get; set; }
        public IUserAuthRepository UserAuthRepo { get; set; }
        //public static ValidateFn ValidateFn { get; set; }
        //public IValidator<Registration> RegistrationValidator { get; set; }

        private void AssertUserAuthRepo()
        {
            if (UserAuthRepo == null)
                throw new ConfigurationException("No IUserAuthRepository has been registered in your AppHost.");
        }

        //[CustomAuthenticate("Employee")]
        public EmployeeUserSession Get(CheckAuthentication request)
        {
            if (UserSession.IsAuthenticated)
            {
                Logger.Debug(string.Format("CheckAuth() UserSession Authenticated: EmployeeId: {0}, SessionId: {1}", request.EmployeeId, request.SessionId));
                return UserSession;
            }
            return null;
        }

        //[CustomAuthenticate("Employee")]
        // Do auth
        public EmployeeUserSession Get(AuthenticateEmployee request)
        {
            StringBuilder stuff = new StringBuilder();
            using (var redis = AppHost.Instance.Resolve<IRedisClientsManager>().GetClient())
            {
                var sessionkeys = redis.SearchKeys("urn:iauthsession:*");
                sessionkeys.AddRange(redis.SearchKeys("urn:employeesession:*"));
                foreach (var key in sessionkeys)
                {
                    var session = redis.Get<EmployeeUserSession>(key);
                    if (session != null)
                    {
                        if (session.Id == request.SessionId && session.EmployeeId == (int)request.EmployeeId)
                        {
                            session.IsAuthenticated = true;
                            return session;
                        }
                    }
                }
                return new EmployeeUserSession() {IsAuthenticated = false};
            }
            return null;
        }


        /// <summary>
        /// Create new employee
        /// </summary>
        public object Post(RegisterEmployee request)
        {
            //if (!ValidationFeature.Enabled) //Already gets run
            //    RegistrationValidator.ValidateAndThrow(request, ApplyTo.Post);

            AssertUserAuthRepo();

            //if (ValidateFn != null)
            //{
            //    var validateResponse = ValidateFn(this, HttpMethods.Post, request);
            //    if (validateResponse != null) return validateResponse;
            //}

            EmployeeDTO response = null;
            var session = GetSession();
            var newUserAuth = request.ConvertTo<UserAuth>();
            var currentUser = UserAuthRepo.GetUserAuth(session, null);

            // TODO: Implement autogenerate

            string password = "test123";

            //var registerNewUser = existingUser == null;
            //var user = registerNewUser
            var user =
                            this.UserAuthRepo.CreateUserAuth(newUserAuth, password);
            //: this.UserAuthRepo.UpdateUserAuth(existingUser, newUserAuth, password);

            //if (registerNewUser)
            //{
            // TODO: Create employee and save employeeId
            var employee = Framework.Services.EmployeeService.Instance.GetEmployeeByUserId(currentUser.Id);
            int employeeId = Framework.Services.EmployeeService.Instance.CreateEmployee(user.Id, employee.WorkplaceId);
            response = Framework.Services.EmployeeService.Instance.GetEmployee(employeeId);

            // TODO: Error handling
            //var isHtml = base.RequestContext.ResponseContentType.MatchesContentType("html");
            //if (isHtml)
            //{
            //    if (string.IsNullOrEmpty(request.Continue))
            //        return response;

            //    return new HttpResult(response)
            //    {
            //        Location = request.Continue
            //    };

            return response;
        }
    }
}