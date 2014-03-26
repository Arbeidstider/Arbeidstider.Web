using Arbeidstider.Web.Framework.Session;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    public class ServiceBase : Service
    {
        protected string SessionId { get { return UserSession.SessionId; } }
        protected EmployeeSession UserSession { get { return GetSession() as EmployeeSession; } }
        protected int CurrentWorkplaceId { get { return UserSession.WorkplaceId; } }
    }
}