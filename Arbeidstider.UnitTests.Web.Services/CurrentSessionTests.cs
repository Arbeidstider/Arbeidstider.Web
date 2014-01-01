using System;
using Arbeidstider.UnitTests.Base.Classes;
using Arbeidstider.Web.Services.ServiceModels;
using NUnit.Framework;

namespace Arbeidstider.UnitTests.Web.Services
{
    [TestFixture()]
    public class CurrentSessionTests : TestBase
    {
        [TestCase()]
        public void Test_Login()
        {
        }

        [TestCase()]
        public void Get_Session()
        {
            DoAuth();
            using (var client = GetServiceClient())
            {
                var request = new Arbeidstider.Web.Services.ServiceModels.SessionRequest();
                var response = client.Send<SessionRequestResponse>("GET", "/getsession", request);
                Assert.That(response.AuthSession != null && response.AuthSession.IsAuthenticated);
                Console.WriteLine(response.AuthSession);
            }
        }
         
    }
}
