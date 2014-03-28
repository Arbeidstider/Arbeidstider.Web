//using System;
//using Arbeidstider.UnitTests.Base.Classes;
//using NUnit.Framework;

//namespace Arbeidstider.UnitTests.Web.Services
//{
//    [TestFixture()]
//    public class CurrentSessionTests : TestBase
//    {
//        [TestCase()]
//        public void Test_Login()
//        {
//            DoAuth();
//        }

//        [TestCase()]
//        public void Get_Session()
//        {
//            DoAuth();
//            using (var client = GetServiceClient())
//            {
//                var request = new Arbeidstider.Web.Services.ServiceModels.SessionRequest();
//                var response = client.Get(request);
//                Assert.That(response.AuthSession != null && response.AuthSession.IsAuthenticated);
//                Console.WriteLine(response.AuthSession);
//            }
//        }
         
//    }
//}
