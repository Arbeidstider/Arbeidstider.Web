using System;
using System.Collections.Generic;
using System.Globalization;
using Arbeidstider.Web.Services.ServiceModels;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.FluentValidation;
using ServiceStack.Web;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    public class CreateEmployeeService : Service
    {
        public IUserAuthRepository UserAuthRepo { get; set; }
        public static ValidateFn ValidateFn { get; set; }
        public IValidator<CreateEmployee> RegistrationValidator { get; set; }

        private void AssertUserAuthRepo()
        {
            if (UserAuthRepo == null)
                throw new Exception("No IUserAuthRepository has been registered in your AppHost.");
        }

        [RequiredRole("Administrator", Provider = "Employee")]
        // check manager/workpaceid inside post
        //[RequiredRole("Manager", Provider = "Employee")]
        public object Post(CreateEmployee request)
        {
            AssertUserAuthRepo();

            if (ValidateFn != null)
            {
                var validateResponse = ValidateFn(this, "POST", request);
                if (validateResponse != null) return validateResponse;
            }

            CreateEmployeeResponse response = null;
            var session = this.GetSession();
            var newUserAuth = ToUserAuth(request);
            var roles = new List<string>();
            switch (request.Role)
            {
                case 1: 
                    roles.Add("Administrator");
                    break;
                case 2: 
                    roles.Add("Manager");
                    break;
                case 3: 
                    roles.Add("Employee");
                    break;
            }
            newUserAuth.Roles = roles;
            newUserAuth.UserName = request.FirstName.Substring(0, 3) + request.LastName.Substring(0, 3) + request.BirthDate.Day +
                                   request.BirthDate.Month;
            newUserAuth.PhoneNumber = request.PhoneNumber;

            var existingUser = UserAuthRepo.GetUserAuth(session, null);

            var registerNewUser = existingUser == null;
            var user = registerNewUser
                           ? this.UserAuthRepo.CreateUserAuth(newUserAuth, request.Password)
                           : this.UserAuthRepo.UpdateUserAuth(existingUser, newUserAuth, request.Password);

            if (registerNewUser)
            {
                //EmployeeService.Instance.CreateEmployee();
                //public bool CreateEmployee(string username, Guid userID, string lastname, string firstname, string mobile,
                //                           string birthDate, int workplaceID)
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
                        response = new CreateEmployeeResponse() {
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
                response = new CreateEmployeeResponse() {
                                                              UserId = user.Id.ToString(CultureInfo.InvariantCulture),
                                                              ReferrerUrl = request.Continue
                                                          };
            }


            //return new RegisterEmployeeResponse();
            return response;
        }

        public UserAuth ToUserAuth(CreateEmployee request)
        {
            var to = request.ConvertTo<UserAuth>();
            to.PrimaryEmail = request.Email;
            return to;
        }

        /// <summary>
        /// Logic to update UserAuth from Registration info, not enabled on OnPut because of security.
        /// </summary>
        public object UpdateUserAuth(CreateEmployee request)
        {
            //if (!ValidationFeature.Enabled)
            //RegistrationValidator.ValidateAndThrow(request, ApplyTo.Put);

            if (ValidateFn != null)
            {
                var response = ValidateFn(this, "PUT", request);
                if (response != null) return response;
            }

            var session = base.GetSession();
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