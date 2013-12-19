using System;
using System.Collections.Generic;
using Arbeidstider.Web.Framework;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Services.ServiceModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack;
using ServiceStack.Auth;

namespace Arbeidstider.UnitTests.Base.Classes
{
    [TestClass]
    public class TestBase
    {
        private static bool _testRemote = false;
        protected JsonServiceClient GetServiceClient()
        {
            if (_testRemote)
                return new JsonServiceClient("http://services.arbeidstider.no");

            return new JsonServiceClient("http://localhost:8181");
        }

        protected List<TimesheetDTO> GetTestTimesheets()
        {
            var request = new Timesheets()
                                {
                                    StartDate = new DateTime(2013, 9, 1),
                                    EndDate = new DateTime(2013, 12, 31),
                                    UserID = GetTestUserID()
                                };
            var all = GetServiceClient().Get(request);
            return all.Timesheets;
        }

        protected Guid GetTestUserID()
        {
            return new Guid("62560772-CFD8-4DDB-8CE3-3F37638C4327");
        }

        #region Init / De-init
        [TestCleanup()]
        public void CleanUp()
        {
            
        }

        [TestInitializeAttribute()]
        public void Initialize()
        {
            DoAuth();
            IoC.Initialize();
        }

        private void DoAuth()
        {
            var client = GetServiceClient();
            var request = new Authenticate() {UserName = "johnor1410", Password = "test123", provider = AuthenticateService.CredentialsProvider};
            var response = client.Post(request);
        }

        #endregion

        protected void WriteLine(string label, string value)
        {
            Console.WriteLine(string.Format("{0}: {1}", label, value));
        }

        protected ServiceBundle Services
        {
            get
            {
                return new ServiceBundle();
            }
        }
    }

    #region Support Classes
    public class ServiceBundle
    {
        /*
        public TimesheetService TimesheetService
        {
            get { return TimesheetService.Instance; }
        }
         */
    }
    #endregion
}
