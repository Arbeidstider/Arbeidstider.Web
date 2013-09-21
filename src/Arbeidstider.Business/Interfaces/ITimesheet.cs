using System;

namespace Arbeidstider.Business.Interfaces
{
    public interface ITimesheet
    {
        int EmployerID { get; set; }
        string StartDate { get; set; }
        string EndDate { get; set; }
        DateTime SelectedDay { get; set; }
    }
}
