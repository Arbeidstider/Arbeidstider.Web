using System;
using System.Runtime.Serialization;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    [DataContract]
    public class CreateTimesheet : IReturn<CreateTimesheetResponse>
    {
        [DataMember]
        public DateTime? SelectedDay { get; set; }
        [DataMember]
        public TimeSpan? ShiftStart { get; set; }
        [DataMember]
        public TimeSpan? ShiftEnd { get; set; }
        [DataMember]
        public int? UserId { get; set; }
    }

    [DataContract]
    public class CreateTimesheetResponse
    {
        [DataMember]
        public bool TimesheetCreated { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } //Where Exceptions get auto-serialized
    }
}