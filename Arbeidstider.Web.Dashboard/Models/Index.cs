using Arbeidstider.Web.Framework.Models;
using System.Collections.Generic;

namespace Arbeidstider.Web.Dashboard.Models
{
    public class Index
    {
        public IEnumerable<CalendarDay> TimesheetCalendar { get; set; }
    }
}