using log4net;

namespace Arbeidstider.Web.Framework.Services
{
    public class ServiceBase
    {
        protected internal static readonly ILog Logger = IoC.Resolve<ILog>();
    }
}
