using Arbeidstider.DataServices;
using Arbeidstider.ServiceModels;
using Arbeidstider.DataObjects.DTO;

namespace Arbeidstider.ServiceInterfaces
{
    public class WorkingHoursService : ServiceBase
    {
        public WorkingHoursDTO Get(UpcomingWorkingHours request)
        {
            var upcomingWorkHours = TimesheetService.Instance.GetUpcomingWorkingHours(request.EmployeeId);
            return upcomingWorkHours;
        }
    }
}