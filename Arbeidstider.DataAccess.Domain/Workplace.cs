using Arbeidstider.Interfaces;

namespace Arbeidstider.DataAccess.Domain
{
    public class Workplace : IWorkplace
    {
        public int WorkplaceID { get; set; }
        public string Name { get; set; }
        public IEmployee Manager { get; set; }
    }
}