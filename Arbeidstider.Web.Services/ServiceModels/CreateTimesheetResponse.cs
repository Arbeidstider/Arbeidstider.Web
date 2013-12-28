using System.Runtime.Serialization;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    [DataContract]
    public class CreateTimesheetResponse
    {
        [DataMember]
        public bool TimesheetCreated { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } //Where Exceptions get auto-serialized
    }
}