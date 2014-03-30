using System;
using System.Collections.Generic;
using System.Linq;
using Arbeidstider.Repository.Constants;
using Arbeidstider.DataInterfaces.Repository;
using Arbeidstider.DataObjects.DAO;
using Dapper;

namespace Arbeidstider.Repository
{
    public class ScheduleRepository : Repository<ScheduleDAO>
    {
        public ScheduleRepository() : base("EmployeeSchedules")
        {
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<ScheduleDAO> GetAll(object parameters)
        {
            var dps = (DynamicParameters) parameters;
            var dt = base.GetMultiple(StoredProcedures.GET_ALL_SCHEDULES_IN_RANGE, dps);
            var schedules = dt as ScheduleDAO[] ?? dt.ToArray();
            //Logger.Debug("Got schedules: " + schedules);

            if (schedules == null || !schedules.Any())
                throw new Exception(string.Format("{0} returned 0 rows.", StoredProcedures.GET_ALL_SCHEDULES_IN_RANGE));

            return schedules;
        }

        public override int Create(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public override ScheduleDAO Get(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public override bool Update(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public override bool Exists(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public override bool Delete(object parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}