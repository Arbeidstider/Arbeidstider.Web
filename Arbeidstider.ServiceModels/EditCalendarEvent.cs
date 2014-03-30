using System.Collections.Generic;
using Arbeidstider.DataObjects.DTO;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    [Route("/calendar/event/edit")]
    [Route("/calendar/event/edit/{Id}")]
    public class EditCalendarEvent : IReturn<List<ScheduleEventDTO>>
    {
        public int Id { get; set; }
    }
}