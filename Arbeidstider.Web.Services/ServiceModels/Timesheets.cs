using System;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    public class Timesheets : IReturn<TimesheetsResponse>
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? WorkplaceID { get; set; }
        public string UserID { get; set; }
    }
}