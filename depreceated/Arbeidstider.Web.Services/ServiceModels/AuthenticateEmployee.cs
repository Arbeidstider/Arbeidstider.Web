using System.Runtime.Serialization;
using Arbeidstider.Web.Framework.Session;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    [DataContract]
    [Route("/employee/auth", "GET")]
    //[Route("/employee/auth/{EmployeeId}", "GET")]
    public class AuthenticateEmployee : IReturn<EmployeeUserSession>
    {
        [DataMember(Name = "EmployeeId")]
        public int EmployeeId { get; set; }

        [DataMember(Name = "SessionId")]
        public string SessionId { get; set; }
    }
}