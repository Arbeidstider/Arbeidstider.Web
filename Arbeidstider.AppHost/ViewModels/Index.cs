using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace Arbeidstider.Web.ViewModels
{
    public class Index
    {
        public IEnumerable<CalendarDay> TimesheetCalendar { get; set; }
    }
}