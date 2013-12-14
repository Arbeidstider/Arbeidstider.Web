using Arbeidstider.Web.Framework.Services;
using Arbeidstider.Web.Services.ServiceModels;
using ServiceStack;

namespace Arbeidstider.Web.Services
{
    public class TimesheetsService : Service
    {
        private readonly TimesheetService _service;

        public TimesheetsService()
        {
            _service = TimesheetService.Instance;
        }

        public object Get(Timesheets request)
        {
            return new TimesheetsResponse() { Timesheets = _service.GetAllWithinRange(request.StartDate, request.EndDate, request.UserID)};
        }
    }
}