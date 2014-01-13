using System;
using System.Runtime.Serialization;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    [DataContract]
    public class UpdateTimesheet : IReturn<UpdateTimesheetResponse>
    {
        /* EmployeeScheduleEventID in DB */
        [DataMember]
        public int Id { get; set; }

        /* Change ShiftWorker */
        [DataMember]
        public int? UserId { get; set; }

        /* Change Day / Time */
        [DataMember]
        public DateTime? SelectedDate { get; set; }
        [DataMember]
        public TimeSpan? ShiftStart { get; set; }
        [DataMember]
        public TimeSpan? ShiftEnd { get; set; }
    }

    [DataContract]
    public class UpdateTimesheetResponse : IHasResponseStatus
    {
        [DataMember]
        public bool TimesheetUpdated { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
}