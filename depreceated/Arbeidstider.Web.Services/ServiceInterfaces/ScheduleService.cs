using System;
using System.Collections.Generic;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Framework.Helpers;
using Arbeidstider.Web.Framework.Services;
using Arbeidstider.Web.Services.Helpers;
using Arbeidstider.Web.Services.ServiceModels;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    public class ScheduleService : ServiceBase
    {
        public object Get(FindSchedules request)
        {
            var cacheKey = UrnId.Create<FindSchedules>(new { WorkplaceId = UserSession.WorkplaceId, From = request.from, To = request.to });
            DateTime startDate = request.from.ToDate();
            DateTime endDate = request.to.ToDate();
            //var response = new List<ScheduleEventDTO>();
            return base.Request.ToOptimizedResultUsingCache(this.GetCacheClient(),
                                                            CacheKeys.FindSchedules.WithParameters(
                                                                UserSession.WorkplaceId, startDate, endDate),
() =>
{
    var response = new FindSchedulesResponse()
        {
            success = 1,
            result = new List<ScheduleEventDTO>()
                        {
                            new ScheduleEventDTO(null)
                                {
                                    id = 293,
                                    title =
                                        "This is warning class event with very long title to check how it fits to evet in day view",
                                    url = "http://www.example.com/",
                                    Class = "event-warning",
                                    start = 1362938400000,
                                    end = 1363197686300,
                                },
                            new ScheduleEventDTO(null)
                                {
                                    id = 294,
                                    title =
                                        "This is warning class event with very long title to check how it fits to evet in day view",
                                    url = "http://www.example.com/",
                                    Class = "event-warning",
                                    start = DateTime.Now.ToMilliSeconds(),
                                    end = DateTime.Now.ToMilliSeconds(),
                                },
                            new ScheduleEventDTO(null)
                                {
                                    id = 294,
                                    title =
                                        "This is warning class event with very long title to check how it fits to evet in day view",
                                    url = "http://www.example.com/",
                                    Class = "event-warning",
                                    start = DateTime.Now.AddDays(1).ToMilliSeconds(),
                                    end = DateTime.Now.AddDays(2).ToMilliSeconds(),
                                },
                        }
        };

    return response;
});
        }
    }
}
