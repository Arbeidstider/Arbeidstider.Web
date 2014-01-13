using System;
using System.Collections.Generic;
using Arbeidstider.Web.Framework.DTO;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    public class Timesheets : IReturn<TimesheetsResponse>
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string UserId { get; set; }
        public int? WorkplaceId { get; set; }
    }

    public class TimesheetsResponse
    {
        public List<TimesheetDTO> Timesheets { get; set; }
        public ResponseStatus ResponseStatus { get; set; } //Where Exceptions get auto-serialized
    }
}