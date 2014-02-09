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
                DateTime startDate;
                DateTime endDate;
                if (DateTime.TryParse(request.StartDate, out startDate) && DateTime.TryParse(request.EndDate, out endDate))
                {
                    response.Timesheets = _service.GetAllWithinRange(
                        startDate,
                        endDate,
                        request.EmployeeId,
                        request.WorkplaceId);
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