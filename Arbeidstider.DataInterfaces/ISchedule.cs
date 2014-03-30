using System;

namespace Arbeidstider.DataInterfaces
{
    public interface ISchedule
    {
         int Id { get; set; }
         DateTime Date { get; set; }
         int EmployeeId { get; set; }
         TimeSpan Start { get; set; }
         TimeSpan Finish { get; set; }
    }
}
