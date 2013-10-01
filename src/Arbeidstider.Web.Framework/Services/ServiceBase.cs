using Arbeidstider.Business.Interfaces.Caching;
using log4net;

namespace Arbeidstider.Web.Framework.Services
{
    public abstract class ServiceBase
    {
        protected internal static readonly ILog Logger = IoC.Resolve<ILog>();

        protected internal static ICacheService Cache
        {
            get
            {
                return IoC.Resolve<ICacheService>();
            }
        }
    }
}
