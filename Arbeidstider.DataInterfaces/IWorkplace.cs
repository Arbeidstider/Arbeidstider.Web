namespace Arbeidstider.DataInterfaces
{
    public interface IWorkplace : IDataObject
    {
        string Name { get; set; }
        IEmployee Manager { get; set; }
    }
}
