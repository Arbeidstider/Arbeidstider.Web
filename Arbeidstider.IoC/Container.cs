using Arbeidstider.DataAccess.Repository;
using Arbeidstider.Interfaces;
using Autofac;
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

                // Repositories
                builder.RegisterType<EmployeeRepository>().As<IRepository<IEmployee>>().SingleInstance();
                builder.RegisterType<TimesheetRepository>().As<IRepository<ITimesheet>>().SingleInstance();
                builder.RegisterType<ScheduleRepository>().As<IRepository<ISchedule>>().SingleInstance();

                // Services
                builder.Register(x => LogManager.GetLogger("FileLogger")).As<ILog>().SingleInstance();

                BaseContainer = builder.Build();
            }
        }

        public static TService Resolve<TService>()
        {
            return BaseContainer.Resolve<TService>();
        }
    }
}
