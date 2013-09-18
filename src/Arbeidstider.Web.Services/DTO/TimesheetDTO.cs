using System;
using System.Runtime.Serialization;

namespace Arbeidstider.Web.Services.DTO
{
    [DataContract]
    public class TimesheetDTO
    {
        public TimesheetDTO(DateTime shiftStart, DateTime shiftEnd, int employerID, EmployerDTO employer = null)
        {
            ShiftStart = shiftStart;
            ShiftEnd = shiftEnd;
            EmployerID = employerID;
            if (employer != null) Employer = employer;
        }

        private int _employerID = 0;

        [DataMember]
        public int EmployerID
        {
            get
            {
                if (Employer != null) 
                    return Employer.EmployerID;

                return _employerID;
            }
            set
            {
                _employerID = value;
            }
        }
        
        [DataMember]
        public EmployerDTO Employer { get; set; }
        [DataMember]
        public DateTime ShiftStart { get; set; }
        [DataMember]
        public DateTime ShiftEnd { get; set; }
    }
}
