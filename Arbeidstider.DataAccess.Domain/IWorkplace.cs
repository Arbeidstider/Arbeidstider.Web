using Arbeidstider.DataAccess.Domain;

namespace Arbeidstider.DataAccess.Domain
{
    public interface IWorkplace
    {
        int WorkplaceID { get; set; }
        string Name { get; set; }
        IEmployee Manager { get; set; }
    }
}
