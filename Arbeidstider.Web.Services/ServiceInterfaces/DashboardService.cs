using Arbeidstider.Web.Services.ServiceModels;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    public class DashboardService : ServiceInterfaceBase
    {
        public object Any(int employeeId)
        {
            var model = new Dashboard();
            return model;
        }
    }
}