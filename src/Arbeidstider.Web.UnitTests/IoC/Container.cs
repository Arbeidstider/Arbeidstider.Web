using System;
using System.Web;
using Arbeidstider.Business.Interfaces.Caching;
using Arbeidstider.Business.Interfaces.Database;
using Arbeidstider.Business.Interfaces.Domain;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.Caching;
using Arbeidstider.Business.Logic.Repository;
using Autofac;
using log4net;

namespace Arbeidstider.Web.UnitTests.IoC
{
    public static class Container
    {
        public static IContainer BaseContainer { get; private set; }

        public static void Build()
        {
            if (BaseContainer == null)
            {
                var builder = new ContainerBuilder();

                // Caching
                builder.RegisterType<CacheService>().As<ICacheService>().SingleInstance();

                // Services
                builder.Register(x => LogManager.GetLogger("FileLogger")).As<ILog>().SingleInstance();
                builder.Register(x => new CacheService(HttpContext.Current.IsDebuggingEnabled ? 
                    new DateTime() : DateTime.UtcNow.AddHours(8))).As<ICacheService>().SingleInstance();

                // Database
                builder.Register(x => new DatabaseConnectionTest("test")).
                    As<IDatabaseConnection>().
                    SingleInstance();

                // Repositories
                builder.Register(x => new EmployeeRepository(Resolve<IDatabaseConnection>())).As<IRepository<IEmployee>>().SingleInstance();
                builder.Register(x => new TimesheetRepository(Resolve<IDatabaseConnection>())).As<IRepository<ITimesheet>>().SingleInstance();

                BaseContainer = builder.Build();
            }
        }

        public static TService Resolve<TService>()
        {
            return BaseContainer.Resolve<TService>();
        }
    }
}
