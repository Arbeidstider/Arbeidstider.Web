using Arbeidstider.Business.Logic.IoC;

namespace Arbeidstider.Web.Framework
{
    public static class IoC
    {
        public static Autofac.IContainer BaseContainer 
        {
            get
            {
                return Container.BaseContainer;
            }
        }

        public static void Initialize()
        {
            Container.Build();
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
