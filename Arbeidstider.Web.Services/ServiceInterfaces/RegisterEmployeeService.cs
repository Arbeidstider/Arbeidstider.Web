using System;
using System.Configuration;
using System.Globalization;
using System.Net.Mime;
using System.Runtime.Serialization;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.FluentValidation;
using ServiceStack.Host;
using ServiceStack.Validation;
using ServiceStack.Web;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    [DataContract]
    public class Registration : IReturn<RegistrationResponse>
    {
        [DataMember(Order = 1)] public string UserName { get; set; }
        [DataMember(Order = 2)] public string FirstName { get; set; }
        [DataMember(Order = 3)] public string LastName { get; set; }
        [DataMember(Order = 4)] public string DisplayName { get; set; }
        [DataMember(Order = 5)] public string Email { get; set; }
        [DataMember(Order = 6)] public string Password { get; set; }
        [DataMember(Order = 7)] public bool? AutoLogin { get; set; }
        [DataMember(Order = 8)] public string Continue { get; set; }
    }

    [DataContract]
    public class RegistrationResponse
    {
        public RegistrationResponse()
        {
            this.ResponseStatus = new ResponseStatus();
        }

        [DataMember(Order = 1)] public string UserId { get; set; }
        [DataMember(Order = 2)] public string SessionId { get; set; }
        [DataMember(Order = 3)] public string UserName { get; set; }
        [DataMember(Order = 4)] public string ReferrerUrl { get; set; }
        [DataMember(Order = 5)] public ResponseStatus ResponseStatus { get; set; }
    }

    public class FullRegistrationValidator : RegistrationValidator
    {
        public FullRegistrationValidator()
        {
            RuleSet(ApplyTo.Post, () => {
                RuleFor(x => x.DisplayName).NotEmpty();
            });
        }
    }

    public class RegistrationValidator : AbstractValidator<Registration>
    {
        public IUserAuthRepository UserAuthRepo { get; set; }

        public RegistrationValidator()
        {
            RuleSet(ApplyTo.Post, () => {
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty().When(x => x.Email.IsNullOrEmpty());
                RuleFor(x => x.Email).NotEmpty().EmailAddress().When(x => x.UserName.IsNullOrEmpty());
                RuleFor(x => x.UserName)
                    .Must(x => UserAuthRepo.GetUserAuthByUserName(x) == null)
                    .WithErrorCode("AlreadyExists")
                    .WithMessage("UserName already exists")
                    .When(x => !x.UserName.IsNullOrEmpty());
                RuleFor(x => x.Email)
                    .Must(x => x.IsNullOrEmpty() || UserAuthRepo.GetUserAuthByUserName(x) == null)
                    .WithErrorCode("AlreadyExists")
                    .WithMessage("Email already exists")
                    .When(x => !x.Email.IsNullOrEmpty());
            });
            RuleSet(ApplyTo.Put, () => {
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
            });
        }
    }

    [DefaultRequest(typeof(Registration))]
    public class RegistrationService : Service
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
        public object Post(Registration request)
        {
            //if (!ValidationFeature.Enabled) //Already gets run
            //    RegistrationValidator.ValidateAndThrow(request, ApplyTo.Post);

            AssertUserAuthRepo();

            if (ValidateFn != null)
            {
                var validateResponse = ValidateFn(this, HttpMethods.Post, request);
                if (validateResponse != null) return validateResponse;
            }

            RegistrationResponse response = null;
            var session = this.GetSession();
            var newUserAuth = ToUserAuth(request);
            var existingUser = UserAuthRepo.GetUserAuth(session, null);

            var registerNewUser = existingUser == null;
            var user = registerNewUser
                ? this.UserAuthRepo.CreateUserAuth(newUserAuth, request.Password)
                : this.UserAuthRepo.UpdateUserAuth(existingUser, newUserAuth, request.Password);

            if (registerNewUser)
            {
                int? employeeId;
                // TODO: Create employee and save employeeId
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
                        response = new RegistrationResponse {
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
                response = new RegistrationResponse {
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

//using System;
//using System.Globalization;
//using Arbeidstider.Web.Framework.Services;
//using Arbeidstider.Web.Services.ServiceModels;
//using ServiceStack;
//using ServiceStack.Auth;
//using ServiceStack.FluentValidation;
//using ServiceStack.Web;

//namespace Arbeidstider.Web.Services.ServiceInterfaces
//{
//    public class RegistrationValidator : AbstractValidator<RegisterEmployee>
//    {
//        public IUserAuthRepository UserAuthRepo { get; set; }

//        public RegistrationValidator()
//        {
//            RuleSet(ApplyTo.Post, () => {
//                RuleFor(x => x.Password).NotEmpty();
//                RuleFor(x => x.UserName).NotEmpty().When(x => x.Email.IsNullOrEmpty());
//                RuleFor(x => x.Email).NotEmpty().EmailAddress().When(x => x.UserName.IsNullOrEmpty());
//                RuleFor(x => x.UserName)
//                    .Must(x => UserAuthRepo.GetUserAuthByUserName(x) == null)
//                    .WithErrorCode("AlreadyExists")
//                    .WithMessage("UserName already exists")
//                    .When(x => !x.UserName.IsNullOrEmpty());
//                RuleFor(x => x.Email)
//                    .Must(x => x.IsNullOrEmpty() || UserAuthRepo.GetUserAuthByUserName(x) == null)
//                    .WithErrorCode("AlreadyExists")
//                    .WithMessage("Email already exists")
//                    .When(x => !x.Email.IsNullOrEmpty());
//            });
//            RuleSet(ApplyTo.Put, () => {
//                RuleFor(x => x.UserName).NotEmpty();
//                RuleFor(x => x.Email).NotEmpty();
//            });
//        }
//    }

//    [CustomAuthenticate("Employee")]
//    public class RegisterEmployeeService : Service
//    {
//        public IUserAuthRepository UserAuthRepo { get; set; }
//        public static ValidateFn ValidateFn { get; set; }

//        public IValidator<RegisterEmployee> RegistrationValidator { get; set; }

//        private void AssertUserAuthRepo()
//        {
//            /*
//            if (UserAuthRepo == null)
//                throw new ConfigurationException("No IUserAuthRepository has been registered in your AppHost.");
//             */
//        }

//        /// <summary>
//        /// Create new Registration
//        /// </summary>
//        public object Post(RegisterEmployee request)
//        {
//            // Check permissions for workplaceID
//            request.UserName = request.FirstName.Substring(0, 3) + request.LastName.Substring(0, 3) +
//                               request.BirthDate.Day + request.BirthDate.Month;
//            request.Password = "test123";
//            /*
//            if (!ValidationFeature.Enabled) //Already gets run
//                RegistrationValidator.ValidateAndThrow(request, ApplyTo.Post);
//             */

//            AssertUserAuthRepo();

//            if (ValidateFn != null)
//            {
//                var validateResponse = ValidateFn(this, HttpMethods.Post, request);
//                if (validateResponse != null) return validateResponse;
//            }

//            RegisterEmployeeResponse response = null;
//            var session = this.GetSession();
//            var newUserAuth = ToUserAuth(request);
//            var existingUser = UserAuthRepo.GetUserAuth(session, null);
//            if (existingUser == null)
//            {
//                int employeeId = EmployeeService.Instance.CreateEmployee(newUserAuth.Id, request.WorkplaceId);
//            }

//            var registerNewUser = existingUser == null;
//            var user = registerNewUser
//                ? this.UserAuthRepo.CreateUserAuth(newUserAuth, request.Password)
//                : this.UserAuthRepo.UpdateUserAuth(existingUser, newUserAuth, request.Password);

//            if (registerNewUser)
//            {
//                session.OnRegistered(this);
//            }

//            if (request.AutoLogin.GetValueOrDefault())
//            {
//                using (var authService = base.ResolveService<AuthenticateService>())
//                {
//                    var authResponse = authService.Post(new Authenticate {
//                        UserName = request.UserName ?? request.Email,
//                        Password = request.Password,
//                        Continue = request.Continue
//                    });

//                    if (authResponse is IHttpError)
//                        throw (Exception)authResponse;

//                    var typedResponse = authResponse as AuthenticateResponse;
//                    if (typedResponse != null)
//                    {
//                        response = new RegisterEmployeeResponse {
//                            SessionId = typedResponse.SessionId,
//                            UserName = typedResponse.UserName,
//                            ReferrerUrl = typedResponse.ReferrerUrl,
//                            UserId = user.Id.ToString(CultureInfo.InvariantCulture),
//                        };
//                    }
//                }
//            }

//            if (response == null)
//            {
//                response = new RegisterEmployeeResponse() {
//                    UserId = user.Id.ToString(CultureInfo.InvariantCulture),
//                    ReferrerUrl = request.Continue
//                };
//            }

//            return response;
//        }

//        public UserAuth ToUserAuth(RegisterEmployee request)
//        {
//            var to = request.ConvertTo<UserAuth>();
//            to.PrimaryEmail = request.Email;
//            return to;
//        }

//        /// <summary>
//        /// Logic to update UserAuth from Registration info, not enabled on OnPut because of security.
//        /// </summary>
//        public object UpdateUserAuth(RegisterEmployee request)
//        {
//            /*
//            if (!ValidationFeature.Enabled)
//                RegistrationValidator.ValidateAndThrow(request, ApplyTo.Put);
//             */

//            if (ValidateFn != null)
//            {
//                var response = ValidateFn(this, HttpMethods.Put, request);
//                if (response != null) return response;
//            }

//            var session = this.GetSession();
//            var existingUser = UserAuthRepo.GetUserAuth(session, null);
//            if (existingUser == null)
//            {
//                throw HttpError.NotFound("User does not exist");
//            }

//            var newUserAuth = ToUserAuth(request);
//            UserAuthRepo.UpdateUserAuth(newUserAuth, existingUser, request.Password);

//            return new RegisterEmployeeResponse {
//                UserId = existingUser.Id.ToString(CultureInfo.InvariantCulture),
//            };
//        }
//    }
//}