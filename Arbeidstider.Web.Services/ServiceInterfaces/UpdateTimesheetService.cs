using Arbeidstider.Web.Framework.Services;
using Arbeidstider.Web.Services.ServiceModels;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    public class UpdateTimesheetService : ServiceInterfaceBase
    {
        public object Any(UpdateTimesheet request)
        {
            return new UpdateTimesheetResponse() { 
                TimesheetUpdated = TimesheetService.Instance.UpdateTimesheet(
                    request.Id,
                    request.UserId,
                    request.SelectedDate,
                    request.ShiftStart,
                    request.ShiftEnd
                )};
        }
    }
}