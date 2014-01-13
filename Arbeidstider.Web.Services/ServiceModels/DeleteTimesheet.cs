using System.Runtime.Serialization;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    [DataContract]
    public class DeleteTimesheet : IReturn<DeleteTimesheetResponse>
    {
        [DataMember]
        public int Id { get; set; }
    }

    [DataContract]
    public class DeleteTimesheetResponse
    {
        [DataMember]
        public bool TimesheetDeleted { get; set; }
    }
}