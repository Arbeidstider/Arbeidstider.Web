using System.Collections.Generic;
using Arbeidstider.DataAccess.Domain;

namespace Arbeidstider.Web.Framework.ViewModels.Dashboard
{
    public class Index
    {
        public IEnumerable<IEmployeeShift> Shifts  { get; set; }
    }
}
