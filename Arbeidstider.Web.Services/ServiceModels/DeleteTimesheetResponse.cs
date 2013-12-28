using System.Runtime.Serialization;

namespace Arbeidstider.Web.Services.ServiceModels
{
    [DataContract]
    public class DeleteTimesheetResponse
    {
        [DataMember]
        public bool TimesheetDeleted { get; set; }
    }
}