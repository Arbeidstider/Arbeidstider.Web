using System.Reflection;
using System.Web;
using Arbeidstider.Business.Domain;
using Arbeidstider.Business.Interfaces.Caching;
using Arbeidstider.Business.Interfaces.Database;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Logic.Caching;
using Arbeidstider.Business.Logic.Domain;
using Arbeidstider.Business.Logic.Repository;
using Arbeidstider.Database;
using Autofac;
using Autofac.Integration.Mvc;
using log4net;

namespace Arbeidstider.Business.Logic.IoC
{
    public static class Container
    {
        public static IContainer BaseContainer { get; private set; }

        public static void Build()
        {
            if (BaseContainer == null)
            {
                var builder = new ContainerBuilder();

                builder.RegisterControllers(Assembly.GetCallingAssembly());

                // Repositories
                builder.RegisterType<EmployeeRepository>().As<IRepository<Employee>>().SingleInstance();
                builder.RegisterType<TimesheetRepository>().As<IRepository<Timesheet>>().SingleInstance();

                // Caching
                builder.RegisterType<CacheService>().As<ICacheService>().SingleInstance();

                // Services
                builder.Register(x => LogManager.GetLogger("FileLogger")).As<ILog>().SingleInstance();

                // Database
                builder.Register(x => new DatabaseConnection(HttpContext.Current.IsDebuggingEnabled ? 
                    Database.Constants.ConnectionStrings.DEBUG : Database.Constants.ConnectionStrings.RELEASE)).
                    As<IDatabaseConnection>().
                    SingleInstance();

                BaseContainer = builder.Build();
            }
        }

        public static TService Resolve<TService>()
        {
            return BaseContainer.Resolve<TService>();
        }
    }
}
