using Arbeidstider.DataInterfaces;

namespace Arbeidstider.DataObjects.DAO
{
    public class WorkplaceDAO : EntityBase, IWorkplace
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public IEmployee Manager { get; set; }
    }
}