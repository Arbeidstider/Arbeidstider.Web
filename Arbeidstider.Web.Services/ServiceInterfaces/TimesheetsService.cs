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

        public object Get(Timesheets request)
        {
            var response = new TimesheetsResponse();
            if (request.WorkplaceID != null)
                response.Timesheets = _service.GetWorkplaceTimesheets(request.WorkplaceID, request.StartDate,
                                                                      request.EndDate);
            else
                response.Timesheets = _service.GetAllWithinRange(request.StartDate.Value, request.EndDate.Value,
                                                                 request.UserID.Value);
            return response;
        }
    }
}