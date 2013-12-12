using System;
using System.Reflection;
using System.Web;
using Arbeidstider.Cache;
using Arbeidstider.DataAccess.Domain;
using Arbeidstider.DataAccess.Repository;
using Autofac;
using Autofac.Integration.Mvc;
using log4net;

namespace Arbeidstider.IoC
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
                builder.RegisterType<EmployeeRepository>().As<IRepository<IEmployee>>().SingleInstance();
                builder.RegisterType<TimesheetRepository>().As<IRepository<ITimesheet>>().SingleInstance();

                // Caching
                builder.RegisterType<CacheService>().As<ICacheService>().SingleInstance();

                // Services
                builder.Register(x => LogManager.GetLogger("FileLogger")).As<ILog>().SingleInstance();
                builder.Register(x => new CacheService(HttpContext.Current.IsDebuggingEnabled ? 
                    new DateTime() : DateTime.UtcNow.AddHours(8))).As<ICacheService>().SingleInstance();

                BaseContainer = builder.Build();
            }
        }

        public static TService Resolve<TService>()
        {
            return BaseContainer.Resolve<TService>();
        }
    }
}
