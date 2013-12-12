using Arbeidstider.Web.Framework.Services;
using log4net;

namespace Arbeidstider.Web.Framework.ViewModels
{
    public class BaseViewModel
    {
        protected internal static EmployeeService EmployeeService = EmployeeService.Instance;
        protected internal static ILog Logger = IoC.Resolve<ILog>();
    }
}
