using System.Data;
using Dapper;

namespace Arbeidstider.DataAccess.Repository.Parameters
{
    public class EmployeeParameters
    {
        public static DynamicParameters Create(int? employeeId = null, int? userId = null, int? workplaceId = null, string username = null, bool lazyLoad = false)
        {
            var parameters = new DynamicParameters();
            if (employeeId != null) parameters.Add("EmployeeId", value: employeeId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            if (userId != null) parameters.Add("UserId", value: userId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            if (workplaceId != null) parameters.Add("WorkplaceId", value: workplaceId, dbType: DbType.Int32, direction: ParameterDirection.Input);    
            if (username != null) parameters.Add("Username", value: username, dbType: DbType.String, direction: ParameterDirection.Input);    
            //parameters.Add("LazyLoad", value: lazyLoad, dbType: DbType.Boolean, direction: ParameterDirection.Input);    
            return parameters;
        }
    }
}
