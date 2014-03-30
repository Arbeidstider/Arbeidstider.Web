using System.Collections.Generic;
using System.Runtime.Serialization;
using Arbeidstider.DataObjects.DTO;
using ServiceStack;

namespace Arbeidstider.ServiceModels
{
    [DataContract]
    [Route("/calendar/events", "GET")]
    public class FindSchedules : IReturn<FindSchedulesResponse>
    {
        [DataMember(Name = "from")]
        public long from { get; set; }
        [DataMember(Name = "to")]
        public long to { get; set; }

        [DataMember(Name = "FilterOnException", IsRequired = false)]
        public short? FilterOnException { get; set; }
    }

    public class FindSchedulesResponse
    {
        public int success { get; set; }
        public List<ScheduleEventDTO> result { get; set; } 
    }
}