using System.Collections.Generic;
using Arbeidstider.Web.Framework.DTO;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    [Route("/calendar/event", "GET")]
    [Route("/calendar/event/{Id}", "GET")]
    public class GetCalendarEvent : IReturn<ScheduleEventDTO>
    {
        public int Id { get; set; }
    }
}