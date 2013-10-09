using Arbeidstider.Business.Interfaces.Domain;

namespace Arbeidstider.Web.Framework.ViewModels.Timesheet
{
    public class TimesheetModel : BaseViewModel
    {
        public TimesheetModel(ITimesheet domain)
        {
            Employee = EmployeeService.GetEmployee(domain.UserID);
            Shift = new EmployeeShift(domain);
            Logger.Debug("Created timesheetmodel with userID: " + domain.UserID);
        }

        public IEmployeeUser Employee { get; set; }
        public IEmployeeShift Shift { get; set; }

        /* Timesheet Properties for JSON */
        /*
        public int? EmployeeScheduleEventID { get; set; }
        public Guid? UserID { get; set; }
        public string Username { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string SelectedDay { get; set; }

        public string ShiftStart { get; set; }
        public string ShiftEnd { get; set; }
        public int? WorkplaceID { get; set; }
         */
    }
}
