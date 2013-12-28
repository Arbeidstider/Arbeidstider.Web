using Arbeidstider.Web.Services.ServiceModels;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    public class UpdateTimesheetService : ServiceInterfaceBase
    {
        public object Any(UpdateTimesheet request)
        {
            return new UpdateTimesheetResponse() { 
                TimesheetUpdated = TimesheetService.UpdateTimesheet(
                    request.TimesheetID,
                    request.UserID,
                    request.SelectedDate,
                    request.ShiftStart,
                    request.ShiftEnd
                )};
        }
    }
}