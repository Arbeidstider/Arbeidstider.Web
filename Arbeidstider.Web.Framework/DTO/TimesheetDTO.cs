using Arbeidstider.DataAccess.Domain;

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
            Id = domain.Id;
        }

        public int Id { get; set; }
        public string ShiftDate { get; private set; }
        public int UserID { get; private set; }
        public string ShiftEnd { get; private set; }

        public string ShiftStart { get; private set; }
    }
}