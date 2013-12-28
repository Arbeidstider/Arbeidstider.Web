using Arbeidstider.Web.Framework.Services;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    public class ServiceInterfaceBase : IService
    {
        protected static TimesheetService TimesheetService { get { return TimesheetService.Instance; }}
    }
}