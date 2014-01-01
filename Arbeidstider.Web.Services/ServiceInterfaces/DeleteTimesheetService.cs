using Arbeidstider.Web.Services.ServiceModels;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    public class DeleteTimesheetService : ServiceInterfaceBase
    {
        public object Any(DeleteTimesheet request)
        {
            return new DeleteTimesheetResponse() {TimesheetDeleted = TimesheetService.Delete(request.TimesheetID)};
        }
    }
}