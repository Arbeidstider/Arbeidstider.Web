using System.Runtime.Serialization;
using ServiceStack;

namespace Arbeidstider.Web.Framework.Session
{
    [DataContract]
    public class EmployeeSession : AuthUserSession
    {
        [DataMember]
        public int EmployeeId { get; set; }
        [DataMember]
        public string SessionId { get; set; }
        [DataMember]
        public int WorkplaceId { get; set; }
        [DataMember]
        public string Stuff { get; set; }
    }
}
