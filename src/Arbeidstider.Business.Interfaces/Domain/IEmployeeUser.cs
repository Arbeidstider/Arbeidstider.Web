namespace Arbeidstider.Business.Interfaces.Domain
{
    public interface IEmployeeUser
    {
        int WorkplaceID { get; set; }
        int Group { get; set; }
        string Fullname { get; set; }
        bool IsAdmin();
        bool IsManager();
        bool HasAccessToWorkplace(int workplaceID);
    }

}
