using System;
using System.Runtime.Serialization;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    [DataContract]
    public class RegisterEmployee : IReturn<RegisterEmployeeResponse>
    {
        [DataMember(Order = 1)]
        public string UserName { get; set; }

        [DataMember(Order = 2)]
        public string FirstName { get; set; }

        [DataMember(Order = 3)]
        public string LastName { get; set; }

        [DataMember(Order = 4)]
        public string DisplayName { get; set; }

        [DataMember(Order = 5)]
        public string Email { get; set; }

        [DataMember(Order = 6)]
        public string Password { get; set; }

        [DataMember(Order = 7)]
        public bool? AutoLogin { get; set; }

        [DataMember(Order = 8)]
        public string Continue { get; set; }

        [DataMember(Order = 9)]
        public DateTime BirthDate { get; set; }

        [DataMember(Order = 10)]
        public int WorkplaceId { get; set; }
    }

    public class RegisterEmployeeResponse
    {
        public RegisterEmployeeResponse()
        {
            this.ResponseStatus = new ResponseStatus();
        }

        [DataMember(Order = 1)] public string UserId { get; set; }
        [DataMember(Order = 2)] public string SessionId { get; set; }
        [DataMember(Order = 3)] public string UserName { get; set; }
        [DataMember(Order = 4)] public string ReferrerUrl { get; set; }
        [DataMember(Order = 5)] public ResponseStatus ResponseStatus { get; set; }
    }
}