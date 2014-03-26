using System.Runtime.Serialization;
using Arbeidstider.DataAccess.Domain;
using Arbeidstider.Interfaces;

namespace Arbeidstider.Web.Framework.DTO
{
    [DataContract]
    public class ShiftDTO
    {
        public ShiftDTO(ITimesheet timesheet)
        {
            Id = timesheet.Id;
            var shift = new EmployeeShift(timesheet);
            Init(shift);
        }
        public ShiftDTO(IEmployeeShift shift)
        {
            Init(shift);
        }

        private void Init(IEmployeeShift shift)
        {
            DayOfWeek = shift.DayOfWeek;
            if (shift.ShiftDate != null) ShiftDate = shift.ShiftDate.Value.Date.ToShortDateString();
            EmployeeId = shift.EmployeeId ?? 0;
            ShiftEnd = shift.ShiftEnd.ToString();
            ShiftStart = shift.ShiftEnd.ToString();
        }

        public int Id { get; set; }
        public int DayOfWeek { get; set; }
        public string ShiftDate { get; set; }
        public int EmployeeId { get; set; }
        public string ShiftEnd { get; set; }
        public string ShiftStart { get; set; }
    }
}
