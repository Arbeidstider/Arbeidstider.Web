using System;
using System.Runtime.Serialization;
using ServiceStack;

namespace Arbeidstider.Web.Framework.Session
{
    [DataContract]
    public class EmployeeSession : AuthUserSession
    {
        [DataMember]
        public string SessionId { get { return base.Id; } }
        [DataMember]
        public int EmployeeId { get; set; }
        [DataMember]
        public int WorkplaceId { get; set; }
    }
}
