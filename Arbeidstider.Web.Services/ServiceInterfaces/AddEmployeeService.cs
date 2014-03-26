using System;
using System.Configuration;
using System.Globalization;
using Arbeidstider.Web.Framework.Services;
using Arbeidstider.Web.Framework.Session;
using Arbeidstider.Web.Services.ServiceModels;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.FluentValidation;
using ServiceStack.Web;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    public class AddEmployeeService : Service
    {
        public IUserAuthRepository UserAuthRepo { get; set; }
        //public static ValidateFn ValidateFn { get; set; }

        //public IValidator<Registration> RegistrationValidator { get; set; }

        private void AssertUserAuthRepo()
        {
            if (UserAuthRepo == null)
                throw new ConfigurationException("No IUserAuthRepository has been registered in your AppHost.");
        }

        /// <summary>
        /// Create new Registration
        /// </summary>
        public object Any(AddEmployee request)
        {
            //if (!ValidationFeature.Enabled) //Already gets run
            //    RegistrationValidator.ValidateAndThrow(request, ApplyTo.Post);

            AssertUserAuthRepo();

            //if (ValidateFn != null)
            //{
            //    var validateResponse = ValidateFn(this, HttpMethods.Post, request);
            //    if (validateResponse != null) return validateResponse;
            //}

            AddEmployeeResponse response = null;
            var session = GetSession();
            var newUserAuth = request.ConvertTo<UserAuth>();
            var existingUser = UserAuthRepo.GetUserAuth(session, null);

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
            var employee = Framework.Services.EmployeeService.Instance.GetEmployeeByUserId(existingUser.Id);
            int employeeId = Framework.Services.EmployeeService.Instance.CreateEmployee(user.Id, employee.WorkplaceId);
            //session.OnRegistered(this);
            //}

            /*
            if (request.AutoLogin.GetValueOrDefault())
            {
                using (var authService = base.ResolveService<AuthenticateService>())
                {
                    var authResponse = authService.Post(new Authenticate() {
                        UserName = request.UserName ?? request.Email,
                        Password = request.Password,
                        Continue = request.Continue
                    });

                    if (authResponse is IHttpError)
                        throw (Exception)authResponse;

                    var typedResponse = authResponse as AuthenticateResponse;
                    if (typedResponse != null)
                    {
                        response = new RegisterEmployeeResponse {
                            SessionId = typedResponse.SessionId,
                            UserName = typedResponse.UserName,
                            ReferrerUrl = typedResponse.ReferrerUrl,
                            UserId = user.Id.ToString(CultureInfo.InvariantCulture),
                        };
                    }
                }
            }*/

            if (response == null)
            {
                response = new AddEmployeeResponse()
                {
                    UserId = user.Id.ToString(CultureInfo.InvariantCulture)
                };
            }

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

        public UserAuth ToUserAuth(Registration request)
        {
            var to = request.ConvertTo<UserAuth>();
            to.PrimaryEmail = request.Email;
            return to;
        }

        /// <summary>
        /// Logic to update UserAuth from Registration info, not enabled on OnPut because of security.
        /// </summary>
        public object UpdateUserAuth(Registration request)
        {
            //if (!ValidationFeature.Enabled)
            //    RegistrationValidator.ValidateAndThrow(request, ApplyTo.Put);

            //if (ValidateFn != null)
            //{
            //    var response = ValidateFn(this, HttpMethods.Put, request);
            //    if (response != null) return response;
            //}

            //var session = this.GetSession();
            //var existingUser = UserAuthRepo.GetUserAuth(session, null);
            //if (existingUser == null)
            //{
            //    throw HttpError.NotFound("User does not exist");
            //}

            //var newUserAuth = ToUserAuth(request);
            //UserAuthRepo.UpdateUserAuth(newUserAuth, existingUser, request.Password);

            //return new RegistrationResponse {
            //    UserId = existingUser.Id.ToString(CultureInfo.InvariantCulture),
            //};
            return null;
        }
    }
}