using ServiceStack;
using ServiceStack.Logging;

namespace Arbeidstider.DataServices
{
    public class ServiceBase
    {
        protected internal static readonly ILog Logger = LogManager.GetLogger("DataServiceLogger");
    }
}
