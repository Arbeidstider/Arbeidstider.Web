using System;
using System.Runtime.Serialization;
using Arbeidstider.Web.Framework.DTO;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    [Route("/employee", "POST")]
    [Route("/employee/register", "POST")]
    public class RegisterEmployee : IReturn<EmployeeDTO>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        // Autogen
        //public string Password { get; set; }
        //public string UserName { get; set; }
    }
    public class RegisterEmployeeResponse : IHasResponseStatus
    {
        public RegisterEmployeeResponse()
        {
            this.ResponseStatus = new ResponseStatus();
        }

        [DataMember(Order = 1)]
        public int EmployeeId { get; set; }
        //[DataMember(Order = 2)] public string SessionId { get; set; }
        //[DataMember(Order = 3)] public string UserName { get; set; }
        //[DataMember(Order = 4)] public string ReferrerUrl { get; set; }
        [DataMember(Order = 5)]
        public ResponseStatus ResponseStatus { get; set; }
    }
}