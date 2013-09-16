using System;
using System.Runtime.Serialization;

namespace Arbeidstider.Services.DTO
{
    [DataContract]
    public class TimesheetDTO
    {
        /*
        public TimesheetDTO(int employeeID, DateTime shiftStart, DateTime shiftEnd)
        {
            EmployeeID = employeeID;
            ShiftStart = shiftStart;
            ShiftEnd = shiftEnd;
        }
         */

        public TimesheetDTO(DateTime shiftStart, DateTime shiftEnd)
        {
            ShiftStart = shiftStart;
            ShiftEnd = shiftEnd;
        }

        public int EmployeeID { get; set; }
        [DataMember]
        public DateTime ShiftStart { get; set; }
        [DataMember]
        public DateTime ShiftEnd { get; set; }
    }
}
