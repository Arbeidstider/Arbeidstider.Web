using Arbeidstider.Interfaces;

namespace Arbeidstider.Web.Framework.DTO
{
    public class TimesheetDTO : ITinyModel
    {
        public TimesheetDTO(ITimesheet domain)
        {
            UserId = domain.UserId;

            ShiftDate = domain.ShiftDate.ToString();
            ShiftEnd = domain.ShiftEnd.ToString();
            ShiftStart = domain.ShiftStart.ToString();
            Id = domain.Id;
        }

        public int Id { get; set; }
        public string ShiftDate { get; private set; }
        public int UserId { get; private set; }
        public string Fullname { get; private set; }
        public string ShiftEnd { get; private set; }

        public string ShiftStart { get; private set; }
        public bool IsTiny { get; set; }
    }
}