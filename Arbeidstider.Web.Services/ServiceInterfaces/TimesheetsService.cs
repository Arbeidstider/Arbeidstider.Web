using System;
using System.Collections.Generic;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Services.ServiceModels;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    //[CustomAuthenticate("EmployeeAuth")]
    public class TimesheetsService : ServiceInterfaceBase
    {
        private readonly Framework.Services.TimesheetService _service;

        public TimesheetsService()
        {
            _service = Framework.Services.TimesheetService.Instance;
        }

        public object Options(Timesheets request)
        {
            return Get(request);
        }
        public object Get(Timesheets request)
        {
            var response = new TimesheetsResponse();
            try
            {
                if (request.WorkplaceId != null && request.WorkplaceId != 0)
                {
                    response.Timesheets = _service.GetWorkplaceTimesheets((int) request.WorkplaceId,
                                                                          DateTime.Parse(request.StartDate),
                                                                          DateTime.Parse(request.EndDate));
                }
                else
                {
                    response.Timesheets = _service.GetAllWithinRange(DateTime.Parse(request.StartDate),
                                                                     DateTime.Parse(request.EndDate),
                                                                     request.UserId != string.Empty
                                                                         ? int.Parse(request.UserId)
                                                                         : 0);
                }
            }
            catch 
            {
                response.Timesheets = new List<TimesheetDTO>();
            }
            return response;
        }    
    }
}