using System.Runtime.Serialization;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    [DataContract]
    public class UpdateTimesheetResponse
    {
        [DataMember]
        public bool TimesheetUpdated { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
}