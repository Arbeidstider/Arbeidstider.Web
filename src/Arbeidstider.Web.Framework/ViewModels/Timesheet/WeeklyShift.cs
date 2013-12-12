using System;
using System.Collections.Generic;
using Arbeidstider.DataAccess.Domain;

namespace Arbeidstider.Web.Framework.Models
{
    public class WeeklyShift
    {
        public WeeklyShift(IEnumerable<IEmployeeShift> shifts)
        {
            
        }

        public DateTime? Monday { get; set; }
        public DateTime? Tuesday { get; set; }
        public DateTime? Wednesday { get; set; }
        public DateTime? Thursday { get; set; }
        public DateTime? Friday { get; set; }
        public DateTime? Saturday { get; set; }
        public DateTime? Sunday { get; set; }
    }
}