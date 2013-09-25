using System;
using System.Collections.Generic;
using Arbeidstider.Business.Logic.Domain;

namespace Arbeidstider.Web.Framework.ViewModels.Timesheet
{
    public class WeeklyTimesheet
    {
        public IEnumerable<KeyValuePair<DateTime, EmployeeShift>> Shifts  { get; set; }
    }
}