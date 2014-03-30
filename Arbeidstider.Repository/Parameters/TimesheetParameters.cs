using System;
using System.Data;
using Dapper;

namespace Arbeidstider.Repository.Parameters
{
    public class TimesheetParameters
    {
        public static DynamicParameters Create(int? id = null, int? employeeId = null, DateTime? startDate = null, DateTime? endDate = null, 
            int? workplaceId = null, TimeSpan? shiftStart = null, TimeSpan? shiftEnd = null, DateTime? shiftDate = null, bool lazyLoad = false)
        {
            var parameters = new DynamicParameters();
            // EmployeeShiftId
            if (id != null) parameters.Add("Id", value: id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            if (employeeId != null) parameters.Add("EmployeeId", value: employeeId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            if (startDate != null) parameters.Add("StartDate", value: startDate, dbType: DbType.Date, direction: ParameterDirection.Input);    
            if (endDate != null) parameters.Add("EndDate", value: endDate, dbType: DbType.Date, direction: ParameterDirection.Input);    
            if (shiftDate != null ) parameters.Add("ShiftDate", value: shiftDate, dbType: DbType.Date, direction: ParameterDirection.Input);    
            if (shiftStart != null) parameters.Add("ShiftStart", value: shiftStart, dbType: DbType.Time, direction: ParameterDirection.Input);    
            if (shiftEnd != null) parameters.Add("ShiftEnd", value: shiftEnd, dbType: DbType.Time, direction: ParameterDirection.Input);    
            if (workplaceId != null) parameters.Add("WorkplaceId", value: workplaceId, dbType: DbType.Int32, direction: ParameterDirection.Input);    
            //parameters.Add("LazyLoad", value: lazyLoad, dbType: DbType.Boolean, direction: ParameterDirection.Input);    
            return parameters;
        }
    }
}