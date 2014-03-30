using Arbeidstider.Repository;
using Arbeidstider.DataInterfaces;
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
