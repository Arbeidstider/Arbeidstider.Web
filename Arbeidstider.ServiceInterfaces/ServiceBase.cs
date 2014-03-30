using Arbeidstider.ServiceModels;
using ServiceStack;

namespace Arbeidstider.ServiceInterfaces
{
    public class ServiceBase : Service
    {
        protected EmployeeSession UserSession { get { return SessionAs<EmployeeSession>(); } }
        protected int CurrentWorkplaceId { get { return UserSession.WorkplaceId; } }
    }
}