using Arbeidstider.Web.Framework.Session;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    public class ServiceBase : Service
    {
        protected string SessionId { get { return UserSession.Id; } }
        protected EmployeeUserSession UserSession { get { return SessionAs<EmployeeUserSession>(); } }
        protected int CurrentWorkplaceId { get { return UserSession.WorkplaceId; } }
    }
}