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
        public Guid? UserID { get; set; }
    }
}