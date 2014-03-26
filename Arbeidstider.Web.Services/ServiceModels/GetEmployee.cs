using Arbeidstider.Web.Framework.Session;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    [Route("/employee/session/get", "GET")]
    public class GetEmployeeSession : IReturn<EmployeeSession>
    {
        public int? EmployeeId { get; set; }
    }
}