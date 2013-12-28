using System;
using Arbeidstider.DataAccess.Domain;
using ServiceStack;

namespace Arbeidstider.Web.Framework.DTO
{
    public class TimesheetDTO
    {
        public TimesheetDTO(ITimesheet domain)
        {
            UserID = domain.UserID;
            ShiftDate = domain.ShiftDate.ToString();
            ShiftEnd = domain.ShiftEnd.ToString();
            ShiftStart = domain.ShiftStart.ToString();
            TimeSheetID = domain.EmployeeShiftID;
        }

        public int TimeSheetID { get; set; }
        public string ShiftDate { get; private set; }
        public Guid UserID { get; private set; }
        public string ShiftEnd { get; private set; }

        public string ShiftStart { get; private set; }
    }
}