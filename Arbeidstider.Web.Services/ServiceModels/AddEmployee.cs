using System;
using System.Runtime.Serialization;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{

    public class AddEmployee : IReturn<AddEmployeeResponse>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string BirthDate { get; set; }

        public string Email { get; set; }

        public string Phonenumber { get; set; }

        public string Address { get; set; }

        public string Zipcode { get; set; }

        public string City { get; set; }

        //[DataMember(Order = 6)]
        //public string Password { get; set; }

        //[DataMember(Order = 1)]
        //public string UserName { get; set; }

        //[DataMember(Order = 4)]
        //public string DisplayName { get; set; }

        public string Continue { get; set; }

        //public int WorkplaceId { get; set; }
    }
    public class AddEmployeeResponse
    {
        public AddEmployeeResponse()
        {
            this.ResponseStatus = new ResponseStatus();
        }

        [DataMember(Order = 1)] public string UserId { get; set; }
        //[DataMember(Order = 2)] public string SessionId { get; set; }
        //[DataMember(Order = 3)] public string UserName { get; set; }
        //[DataMember(Order = 4)] public string ReferrerUrl { get; set; }
        [DataMember(Order = 5)] public ResponseStatus ResponseStatus { get; set; }
    }
}