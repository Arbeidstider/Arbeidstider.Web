using Arbeidstider.Business.Interfaces.Database;
using Autofac;

namespace Arbeidstider.Web.UnitTests.BaseClasses
{
    public class TestBase
    {
        protected internal IContainer Container { get { return IoC.Container.BaseContainer; } }
        protected internal IDatabaseConnection DatabaseTest { get { return new DatabaseConnectionTest(); } }
        public static void Setup()
        {
            IoC.Container.Build();
        }
    }
}
