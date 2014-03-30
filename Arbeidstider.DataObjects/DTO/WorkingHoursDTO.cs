using System;
using System.Runtime.Serialization;

namespace Arbeidstider.DataObjects.DTO
{
    [DataContract]
    public class WorkingHoursDTO
    {
        public WorkingHoursDTO(TimesheetDTO timesheet)
        {
            this.Date = timesheet.ShiftDate;
            this.StartHour = timesheet.ShiftStart;
            this.EndHour = timesheet.ShiftEnd;
            DateTime shiftDate = DateTime.Parse(timesheet.ShiftDate);
            this.Day = ConvertToNorwegianDay((int)shiftDate.DayOfWeek);
        }
        public WorkingHoursDTO(ShiftDTO shift)
        {
            this.Date = shift.ShiftDate;
            this.StartHour = shift.ShiftStart;
            this.EndHour = shift.ShiftEnd;
            this.Day = ConvertToNorwegianDay(shift.DayOfWeek);
        }

        private static string ConvertToNorwegianDay(int dayOfWeek)
        {
            string day = "";
            switch (dayOfWeek)
            {
                case 0:
                    day = "Måndag";
                    break;
                case 1:
                    day = "Tisdag";
                    break;
                case 2:
                    day =  "Onsdag";
                    break;
                case 3:
                    day =  "Torsdag";
                    break;
                case 4:
                    day =  "Fredag";
                    break;
                case 5:
                    day =  "Lørdag";
                    break;
                case 6:
                    day =  "Søndag";
                    break;
                default:
                    break;
            }
            return day;
        }

        // DD.MM.YYYY 22.02.2014
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string StartHour { get; set; }
        [DataMember]
        public string EndHour { get; set; }
        [DataMember]
        public string Day { get; set; }

    }
}
