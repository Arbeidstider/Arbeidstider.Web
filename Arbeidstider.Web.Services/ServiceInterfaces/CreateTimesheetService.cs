using Arbeidstider.Web.Services.Exceptions;
using Arbeidstider.Web.Services.ServiceModels;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    public class CreateTimesheetService : ServiceInterfaceBase
    {
        public object Post(CreateTimesheet request)
        {
            if (request.UserID == null || request.SelectedDay == null
                || request.ShiftStart == null || request.ShiftEnd == null)
            {
                throw new TimesheetServiceException("One or more values are null for TimesheetService.Create");
            }

            return new CreateTimesheetResponse()
                       {
                           TimesheetCreated = TimesheetService.Create(
                               request.UserID.Value,
                               request.SelectedDay.Value,
                               request.ShiftStart.Value,
                               request.ShiftEnd.Value)
                       };
        }
    }
}