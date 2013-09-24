using System.Reflection;
using System.Web;
using Arbeidstider.Business.Domain;
using Arbeidstider.Business.Interfaces.Database;
using Arbeidstider.Business.Interfaces.Repository;
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
                builder.RegisterType<EmployerRepository>().As<IRepository<Employer>>().SingleInstance();
                builder.RegisterType<TimesheetRepository>().As<IRepository<Timesheet>>().SingleInstance();

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
