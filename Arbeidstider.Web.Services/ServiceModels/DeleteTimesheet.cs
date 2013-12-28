using System.Runtime.Serialization;
using ServiceStack;

namespace Arbeidstider.Web.Services.ServiceModels
{
    [DataContract]
    public class DeleteTimesheet : IReturn<DeleteTimesheetResponse>
    {
        [DataMember]
        public int TimesheetID { get; set; }
    }
}