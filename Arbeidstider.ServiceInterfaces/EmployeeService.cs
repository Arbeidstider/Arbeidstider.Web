using System.Configuration;
using System.Text;
using Arbeidstider.ServiceModels;
using Arbeidstider.DataObjects.DTO;
using Arbeidstider.Web.Services.ServiceModels;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Logging;
using ServiceStack.Redis;

namespace Arbeidstider.ServiceInterfaces
{
    //[CustomAuthenticate("Employee")]
    public class EmployeeService : ServiceBase
    {
        //public ILog Logger { get; set; }
        //public IUserAuthRepository UserAuthRepo { get; set; }
        //public static ValidateFn ValidateFn { get; set; }
        //public IValidator<Registration> RegistrationValidator { get; set; }

        private void AssertUserAuthRepo()
        {
            //if (UserAuthRepo == null)
                //throw new ConfigurationException("No IUserAuthRepository has been registered in your AppHost.");
        }

        public CheckAuthenticationResponse Get(CheckAuthentication request)
        {
            var session = base.SessionAs<EmployeeSession>();
            int uid;
            if (session != null && session.IsAuthenticated)
            {
                return new CheckAuthenticationResponse()
                    {
                        IsAuthenticated = true,
                    };
            }
            return new CheckAuthenticationResponse()
                {
                    IsAuthenticated = false,
                };
        }

        //[CustomAuthenticate("Employee")]
        // Do auth
        //public EmployeeSession Get(AuthenticateEmployee request)
        //{
        //    StringBuilder stuff = new StringBuilder();
        //    using (var redis = AppHostBase.Instance.Resolve<IRedisClientsManager>().GetClient())
        //    {
        //        var sessionkeys = redis.SearchKeys("urn:iauthsession:*");
        //        foreach (var key in sessionkeys)
        //        {
        //            var session = redis.Get<EmployeeSession>(key);
        //            if (session != null)
        //            {
        //                if (session.Id == request.SessionId && session.EmployeeId == (int)request.EmployeeId)
        //                {
        //                    session.IsAuthenticated = true;
        //                    return session;
        //                }
        //            }
        //        }
        //        return new EmployeeUserSession() { IsAuthenticated = false };
        //    }
        //    return null;
        //}


        /// <summary>
        /// Create new employee
        /// </summary>
        //public object Post(RegisterEmployee request)
        //{
        //    //if (!ValidationFeature.Enabled) //Already gets run
        //    //    RegistrationValidator.ValidateAndThrow(request, ApplyTo.Post);

        //    AssertUserAuthRepo();

        //    //if (ValidateFn != null)
        //    //{
        //    //    var validateResponse = ValidateFn(this, HttpMethods.Post, request);
        //    //    if (validateResponse != null) return validateResponse;
        //    //}

        //    EmployeeDTO response = null;
        //    var session = GetSession();
        //    var newUserAuth = new UserAuth();
        //    var currentUser = UserAuthRepo.GetUserAuth(session, null);

        //    // TODO: Implement autogenerate

        //    string password = "test123";

        //    //var registerNewUser = existingUser == null;
        //    //var user = registerNewUser
        //    var user =
        //                    this.UserAuthRepo.CreateUserAuth(newUserAuth, password);
        //    //: this.UserAuthRepo.UpdateUserAuth(existingUser, newUserAuth, password);

        //    //if (registerNewUser)
        //    //{
        //    // TODO: Create employee and save employeeId
        //    var employee = DataServices.EmployeeService.Instance.GetEmployeeByUserId(currentUser.Id);
        //    int employeeId = DataServices.EmployeeService.Instance.CreateEmployee(user.Id, employee.WorkplaceId);
        //    response = DataServices.EmployeeService.Instance.GetEmployee(employeeId);

        //    // TODO: Error handling
        //    //var isHtml = base.RequestContext.ResponseContentType.MatchesContentType("html");
        //    //if (isHtml)
        //    //{
        //    //    if (string.IsNullOrEmpty(request.Continue))
        //    //        return response;

        //    //    return new HttpResult(response)
        //    //    {
        //    //        Location = request.Continue
        //    //    };

        //    return response;
        //}
    }
}