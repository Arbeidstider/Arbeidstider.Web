using Arbeidstider.DataObjects.DTO;
using ServiceStack;

namespace Arbeidstider.ServiceModels
{
    [Route("/calendar/event", "GET")]
    [Route("/calendar/event/{Id}", "GET")]
    public class GetCalendarEvent : IReturn<ScheduleEventDTO>
    {
        public int Id { get; set; }
    }
}