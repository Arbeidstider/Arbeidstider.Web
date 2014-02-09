using System.Configuration;
using Arbeidstider.UnitTests.Base.Classes;
using Moq;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.FluentValidation;
using ServiceStack.Host;
using ServiceStack.OrmLite;
using ServiceStack.Testing;

namespace Arbeidstider.UnitTests.Web.Services.Services
{
    [TestFixture]
    public class RegisterServiceTests : TestBase
    {
        static readonly AuthUserSession authUserSession = new AuthUserSession() ;
        private ServiceStackHost appHost;

        //[TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            appHost = new BasicAppHost
            {
                ConfigureContainer = c =>
                {
                    var authService = new AuthenticateService();
                    c.Register(authService);
                    c.Register<IAuthSession>(authUserSession);
                    AuthenticateService.Init(() => authUserSession, new CredentialsAuthProvider());
                    c.Register(new OrmLiteAuthRepository(new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["Auth"].ConnectionString, SqlServerDialect.Provider)).AsUserAuthRepository());
                }
            }.Init();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            //appHost.Dispose();
            appHost = null;
        }

            public static IUserAuthRepository GetStubRepo()
            {
                var mock = new Mock<IUserAuthRepository>();
                //mock.Expect(x => x.GetUserAuthByUserName(It.IsAny<string>()))
                //    .Returns((UserAuth)new UserAuth() { UserName = "johnor1014"});
                //mock.Expect(x => x.CreateUserAuth(It.IsAny<UserAuth>(), It.IsAny<string>()))
                //    .Returns(new UserAuth { Id = 4 });

                return mock.Object;
            }

            public static RegisterService GetRegistrationService(
                AbstractValidator<Register> validator = null,
                IUserAuthRepository authRepo = null,
                string contentType = null)
            {
                var requestContext = new BasicRequest();
                if (contentType != null)
                {
                    requestContext.ResponseContentType = contentType;
                }

                var userAuthRepository = new OrmLiteAuthRepository(new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["Auth"].ConnectionString, SqlServerDialect.Provider)).AsUserAuthRepository();
                var service = new ServiceStack.Auth.RegisterService()
                {
                    RegistrationValidator = validator ?? new RegistrationValidator { UserAuthRepo = userAuthRepository },
                    AuthRepo = userAuthRepository,
                    Request = requestContext,
                };

                HostContext.Container.Register(userAuthRepository);

                return service;
            }

            [TestCase]
            public void Register_First_User()
            {
                var userAuthRepository = new OrmLiteAuthRepository( new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["Auth"].ConnectionString, SqlServerDialect.Provider));
                var existingUser = userAuthRepository.GetUserAuthByUserName("johnor1410");
                var newUser = existingUser;
                newUser.Email = "johan.nordstrom86@gmail.com";
                newUser.PrimaryEmail = "johan.nordstrom86@gmail.com";
                userAuthRepository.UpdateUserAuth(existingUser, newUser, "Test123!");
                return;
                var service = GetRegistrationService();
                var request = new Register()
                {
               UserName = "johnor1410",
               AutoLogin = true,
               Continue = "/",
               DisplayName = "Johan Nordström",
               Email = "lepthone@gmail.com",
               Password = "test123",
               FirstName = "Johan",
               LastName = "Nordström"
           };

            var response = service.Post(request);
            Assert.That(response as RegisterResponse != null);
            /*
            WriteLine("UserID", response.UserId);
            WriteLine("Username", response.UserName);
            WriteLine("Error Code", response.ResponseStatus.ErrorCode);

            Assert.That(response.ResponseStatus.Errors.Count == 0);
             */
        }
    }
}