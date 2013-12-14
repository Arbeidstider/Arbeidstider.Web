using System;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    public class Timesheets : IReturn<TimesheetsResponse>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid UserID { get; set; }
    }
}