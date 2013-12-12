using System;
using Arbeidstider.DataAccess.Domain;
using Arbeidstider.Web.Framework.ViewModels.Account;

namespace Arbeidstider.Web.Framework.ViewModels.Timesheet
{
    public class TimesheetModel : BaseViewModel
    {
        public TimesheetModel(ITimesheet domain)
        {
            Employee = EmployeeService.GetEmployee(domain.UserID);
            Shift = new EmployeeShift(domain);
            SelectedDay = domain.SelectedDay;
            Logger.Debug("Created timesheetmodel with userID: " + domain.UserID);
        }

        public DateTime SelectedDay { get; private set; }
        public EmployeeUser Employee { get; private set; }
        public EmployeeShift Shift { get; private set; }
    }
}