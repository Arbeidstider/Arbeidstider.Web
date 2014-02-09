using Arbeidstider.Interfaces;

namespace Arbeidstider.DataAccess.Repository.Parameters
{
    public class EmployeeParameters : IEmployeeParameters
    {
        public static IEmployeeParameters Create()
        {
            var parameters = new EmployeeParameters();
            return parameters;
        }
    }
}
