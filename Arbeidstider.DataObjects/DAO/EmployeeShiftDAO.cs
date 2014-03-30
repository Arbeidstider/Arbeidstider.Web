using System;
using Arbeidstider.DataInterfaces;
using Arbeidstider.DataObjects.DTO;

namespace Arbeidstider.DataObjects.DAO
{
    public class EmployeeShiftDAO : EntityBase, IEmployeeShift
    {
        public EmployeeShiftDAO(ITimesheet timesheet)
        {
            if (timesheet != null)
            {
                ShiftDate = timesheet.ShiftDate;
                EmployeeId = timesheet.EmployeeId;
                ShiftEnd = timesheet.ShiftEnd;
                ShiftStart = timesheet.ShiftStart;
            }
        }

        public DateTime? ShiftDate { get; set; }
        public int? EmployeeId { get; set; }

        private int _dayOfWeek;
        public int DayOfWeek
        {
            get 
            {
                if (ShiftDate != null) return (int) ShiftDate.Value.DayOfWeek;
                return _dayOfWeek;
            }
            set { _dayOfWeek = value; }
        }

        public TimeSpan ShiftEnd { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public override int Id { get; set; }
    }
}