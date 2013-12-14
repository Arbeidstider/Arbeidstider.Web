using Arbeidstider.Web.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack;

namespace Arbeidstider.UnitTests.Base.Classes
{
    [TestClass]
    public class TestBase
    {
        #region Init / De-init
        [TestCleanup()]
        public void CleanUp()
        {
            
        }

        [TestInitializeAttribute()]
        public void Initialize()
        {
            IoC.Initialize();
        }
        #endregion

        protected ServiceBundle Services
        {
            get
            {
                return new ServiceBundle();
            }
        }

        protected JsonServiceClient GetClient()
        {
            return new JsonServiceClient();
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
