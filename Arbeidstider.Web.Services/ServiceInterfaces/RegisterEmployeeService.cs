using System;
using System.Configuration;
using System.Globalization;
using Arbeidstider.Web.Framework.Services;
using Arbeidstider.Web.Services.ServiceModels;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.FluentValidation;
using ServiceStack.Web;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    [DefaultRequest(typeof(RegisterEmployee))]
    public class RegisterEmployeeService : Service
    {
        public IUserAuthRepository UserAuthRepo { get; set; }
        public static ValidateFn ValidateFn { get; set; }

        public IValidator<Registration> RegistrationValidator { get; set; }

        private void AssertUserAuthRepo()
        {
            if (UserAuthRepo == null)
                throw new ConfigurationException("No IUserAuthRepository has been registered in your AppHost.");
        }

        /// <summary>
        /// Create new Registration
        /// </summary>
        public object Post(RegisterEmployee request)
        {
            //if (!ValidationFeature.Enabled) //Already gets run
            //    RegistrationValidator.ValidateAndThrow(request, ApplyTo.Post);

            AssertUserAuthRepo();

            if (ValidateFn != null)
            {
                var validateResponse = ValidateFn(this, HttpMethods.Post, request);
                if (validateResponse != null) return validateResponse;
            }

            RegisterEmployeeResponse response = null;
            var session = this.GetSession();
            var newUserAuth = request.ConvertTo<UserAuth>();
            var existingUser = UserAuthRepo.GetUserAuth(session, null);

            var registerNewUser = existingUser == null;
            var user = registerNewUser
                           ? this.UserAuthRepo.CreateUserAuth(newUserAuth, request.Password)
                           : this.UserAuthRepo.UpdateUserAuth(existingUser, newUserAuth, request.Password);

            if (registerNewUser)
            {
                // TODO: Create employee and save employeeId
                int employeeId = EmployeeService.Instance.CreateEmployee(user.Id, request.WorkplaceId);
                session.OnRegistered(this);
            }

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
            }

            if (response == null)
            {
                response = new RegisterEmployeeResponse() {
                    UserId = user.Id.ToString(CultureInfo.InvariantCulture),
                    ReferrerUrl = request.Continue
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

            if (ValidateFn != null)
            {
                var response = ValidateFn(this, HttpMethods.Put, request);
                if (response != null) return response;
            }

            var session = this.GetSession();
            var existingUser = UserAuthRepo.GetUserAuth(session, null);
            if (existingUser == null)
            {
                throw HttpError.NotFound("User does not exist");
            }

            var newUserAuth = ToUserAuth(request);
            UserAuthRepo.UpdateUserAuth(newUserAuth, existingUser, request.Password);

            return new RegistrationResponse {
                UserId = existingUser.Id.ToString(CultureInfo.InvariantCulture),
            };
        }
    }
}