using System.Collections.Generic;
using Arbeidstider.Web.Framework.DTO;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    public class TimesheetsResponse
    {
        public List<TimesheetDTO> Timesheets { get; set; }
        public ResponseStatus ResponseStatus { get; set; } //Where Exceptions get auto-serialized
    }
}