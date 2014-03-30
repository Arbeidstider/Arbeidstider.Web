using System.Runtime.Serialization;
using Arbeidstider.DataInterfaces;

namespace Arbeidstider.DataObjects.DTO
{
    [DataContract]
    public class ScheduleEventDTO
    {
        public ScheduleEventDTO(ISchedule domain)
        {
            //this.Id = domain.Id;
        }
        /* Support bootstrap-calendar until fully cust */
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public string Class { get; set; }
        [DataMember]
        public long start { get; set; }
        [DataMember]
        public long end { get; set; }
        //[DataMember]
        //public int Id { get; set; }
        //[DataMember]
        //public EmployeeDTO Employee { get; set; }
        //[DataMember]
        //public int? ReplacementEmployeeId { get; set; }
        //[DataMember]
        //public short? Exception { get; set; }
        //[DataMember]
        //public string ExceptionComment { get; set; }
        //[DataMember]
        //public string Url { get { return "/calendar/event/" + Id; } }
        //[DataMember]
        //public string CssClass { get { return "event-important"; } }
        //[DataMember]
        //public long Start { get; set; }
        //[DataMember]
        //public long End { get; set; }
        //            "id": 293,
        //            "title": "Event 1",
        //            "url": "http://example.com",
        //            "class": "event-important",
        //            "start": 12039485678000, // Milliseconds
        //            "end": 1234576967000 // Milliseconds

    }
}
