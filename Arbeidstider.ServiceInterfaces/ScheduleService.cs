using System;
using System.Collections.Generic;
using Arbeidstider.BusinessLogic.Extensions;
using Arbeidstider.DataObjects.DTO;
using Arbeidstider.ServiceModels;
using Arbeidstider.Web.Services.ServiceModels;
using ServiceStack;
using ServiceStack.Configuration;

namespace Arbeidstider.ServiceInterfaces
{
    public class ScheduleService : ServiceBase
    {
        public object Get(FindSchedules request)
        {
            string cacheTimeout = new AppSettings().GetString("FindSchedules.Monthly");
            var cacheKey = UrnId.Create<FindSchedules>(new { WorkplaceId = UserSession.WorkplaceId, From = request.from, To = request.to });
            int timeout;
            int.TryParse(cacheTimeout, out timeout);
            TimeSpan timeOut = timeout != 0 ? new TimeSpan(0, 0, timeout, 0) : new TimeSpan(0, 0, 10);
            DateTime startDate = request.from.ToDate();
            DateTime endDate = request.to.ToDate();
            //var response = new List<ScheduleEventDTO>();
            return base.Request.ToOptimizedResultUsingCache(ServiceStackHost.Instance.GetCacheClient(),
                                                            cacheKey, timeOut,
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
                        }
        };
    response.result.Add(new ScheduleEventDTO(null)
        {
            id = 294,
            title =
                "This is warning class event with very long title to check how it fits to evet in day view",
            url = "http://www.example.com/",
            Class = "event-warning",
            start = DateTime.Now.AddDays(1).ToMilliSeconds(),
            end = DateTime.Now.AddDays(2).ToMilliSeconds(),
        });

    return response;
});
        }
    }
}
