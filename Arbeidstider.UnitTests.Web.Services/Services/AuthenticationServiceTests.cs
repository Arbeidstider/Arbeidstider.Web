using System;
using Arbeidstider.UnitTests.Base.Classes;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Auth;

namespace Arbeidstider.UnitTests.Web.Services.Services
{
    [TestFixture]
    public class AuthenticationServiceTests : TestBase
    {
        [TestCase]
        public void Test_Authenicate_1()
        {
            var client = GetServiceClient();
            var request = new Authenticate() {UserName = "johnor1410", Password = "Test123!"};
            var response = client.Post(request);
            WriteLine("ErrorCode", response.ResponseStatus.ErrorCode);
            WriteLine("Username", response.UserName);
            WriteLine("SessionID", response.SessionId);
        }
    }
}
