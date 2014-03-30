using Arbeidstider.Web.Framework.Session;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    [Route("/employee/auth/check", "GET")]
    public class CheckAuthentication : IReturn<EmployeeUserSession>
    {
        public int? EmployeeId { get; set; }
        public string SessionId { get; set; }
    }
}