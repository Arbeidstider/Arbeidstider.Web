using System;
using Arbeidstider.Web.Framework.DTO;
using Arbeidstider.Web.Services.ServiceModels;

namespace Arbeidstider.Web.Services.ServiceInterfaces
{
    public class WorkingHoursService : ServiceBase
    {
        public WorkingHoursDTO Get(UpcomingWorkingHours request)
        {
            var upcomingWorkHours = Framework.Services.TimesheetService.Instance.GetUpcomingWorkingHours(request.EmployeeId);
            return upcomingWorkHours;
        }
    }
}