using System.Collections.Generic;
using Arbeidstider.Repository.Constants;
using Arbeidstider.Repository.Exceptions;
using Arbeidstider.DataInterfaces;
using Arbeidstider.DataInterfaces.Repository;
using Arbeidstider.DataObjects.DAO;
using Dapper;

namespace Arbeidstider.Repository
{
    public class TimesheetRepository : Repository<TimesheetDAO>
    {
        public TimesheetRepository() : base("")
        {
        }

        public override void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<TimesheetDAO> GetAll(object parameters)
        {
            return new List<TimesheetDAO>();
            //var dt = base.GetMultiple<Timesheet>(StoredProcedures.GET_ALL_TIMESHEETS, (DynamicParameters)parameters);
            //var timesheets = dt as Timesheet[] ?? dt.ToArray();
            //// try catch
            ////if (dt == null || !timesheets.Any())
            //// throw new TimesheetRepositoryException(string.Format("Failed to GetAll with parameters: {0}", parameters));

            //return timesheets;
        }

        public override int Create(object parameters)
        {
            var dt = base.GetSingle(StoredProcedures.CREATE_TIMESHEET, (DynamicParameters)parameters);

            return dt.Id;
        }

        public override TimesheetDAO Get(object parameters)
        {
            var dt = base.GetSingle(StoredProcedures.GET_TIMESHEET, (DynamicParameters)parameters);

            return dt;
        }

        public override bool Update(object parameters)
        {
            var dt = base.Execute(StoredProcedures.UPDATE_TIMESHEET, (DynamicParameters)parameters);

            if (!dt) throw new TimesheetRepositoryException("Failed to update timesheet");

            return true;
        }

        public override bool Delete(object parameters)
        {
            return base.Execute(StoredProcedures.DELETE_TIMESHEET, (DynamicParameters)parameters);
        }

        public override bool Exists(object parameters)
        {
            return true;
        }
    }
}