using System;
using System.Collections.Generic;
using Arbeidstider.Web.Framework.ViewModels.Timesheet;

namespace Arbeidstider.Web.Framework.ViewModels.Dashboard
{
    public class Index
    {
        public IEnumerable<KeyValuePair<DateTime, EmployeeShift>> Shifts  { get; set; }
    }
}
