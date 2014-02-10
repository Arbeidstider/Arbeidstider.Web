using Arbeidstider.Web.Framework.Services;
using Arbeidstider.Web.Services.Exceptions;
using Arbeidstider.Web.Services.ServiceModels;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    public class CreateTimesheetService : ServiceInterfaceBase
    {
        public object Post(CreateTimesheet request)
        {
            if (request.UserId == null || request.SelectedDay == null
                || request.ShiftStart == null || request.ShiftEnd == null)
            {
                throw new TimesheetServiceException("One or more values are null for TimesheetService.Create");
            }

            int id = TimesheetService.Instance.CreateTimesheet(
                request.UserId.Value,
                request.SelectedDay.Value,
                request.ShiftStart.Value,
                request.ShiftEnd.Value);
            bool created = id != 0;

            return new CreateTimesheetResponse()
                       {
                           TimesheetCreated = created,
                       };
        }
    }
}