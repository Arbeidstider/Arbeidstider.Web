using System.Runtime.Serialization;
using ServiceStack;

namespace Arbeidstider.ServiceModels
{
    [Authenticate]
    [Route("/employee/verify", "GET")]
    [DataContract]
    public class CheckAuthentication : IReturn<CheckAuthenticationResponse>
    {
    }

    public class CheckAuthenticationResponse
    {
        public bool IsAuthenticated { get; set; }
    }
}