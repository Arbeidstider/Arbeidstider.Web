using System.Collections.Generic;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    [Route("/timesheets", "DELETE")]
    [Route("/timesheets/delete", "DELETE")]
    [Route("/timesheets/delete/{Id}", "DELETE")]
    [Route("/timesheets/delete/{Ids}", "DELETE")]
    public class DeleteTimesheet
    {
        public DeleteTimesheet(List<int> Ids)
        {
            this.Ids = Ids;
        }

        public DeleteTimesheet(int Id)
        {
            this.Id = Id;
        }

        public List<int> Ids { get; set; }
        public int? Id { get; set; }
    }
}