using System;
using Arbeidstider.Web.Services.ServiceModels;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    public class TimesheetsService : ServiceInterfaceBase
    {
        private readonly Arbeidstider.Web.Framework.Services.TimesheetService _service;

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
            if (request.WorkplaceID != null)
                response.Timesheets = _service.GetWorkplaceTimesheets(request.WorkplaceID, DateTime.Parse(request.StartDate),
                                                                      DateTime.Parse(request.EndDate));
            else
                response.Timesheets = _service.GetAllWithinRange(DateTime.Parse(request.StartDate), DateTime.Parse(request.EndDate),
                                        request.UserID != string.Empty ? int.Parse(request.UserID) : 0);
            return response;
        }
    }
}