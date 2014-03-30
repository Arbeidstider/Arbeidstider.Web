using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Arbeidstider.Repository.Extensions;
using Arbeidstider.DataInterfaces;
using Arbeidstider.DataObjects;
using Dapper;

namespace Arbeidstider.Repository
{
    public abstract class Repository<T> : IRepository<T> where T: EntityBase
    {
        private readonly string _tableName;

        internal IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["Arbeidstider-Local"].ConnectionString);
            }
        }

        public Repository(string tableName)
        {
            _tableName = tableName;
        }

        internal virtual dynamic Mapping(T item)
        {
            return item;
        }

        public virtual void Add(T item)
        {
            using (IDbConnection cn = Connection)
            {
                var parameters = (object)Mapping(item);
                cn.Open();
                item.Id = cn.Insert<int>(_tableName, parameters);
            }
        }

        public virtual void Update(T item)
        {
            using (IDbConnection cn = Connection)
            {
                var parameters = (object)Mapping(item);
                cn.Open();
                cn.Update(_tableName, parameters);
            }
        }

        public abstract void Delete(int id);
        public abstract IEnumerable<T> GetAll(object parameters);
        public abstract int Create(object parameters);
        public abstract T Get(object parameters);
        public abstract bool Update(object parameters);
        public abstract bool Exists(object parameters);
        public abstract bool Delete(object parameters);

        public virtual void Remove(T item)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute("DELETE FROM " + _tableName + " WHERE ID=@ID", new { ID = item.Id });
            }
        }

        public virtual T FindByID(int id)
        {
            T item = default(T);

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                item = cn.Query<T>("SELECT * FROM " + _tableName + " WHERE IId=@ID", new { Id = id }).SingleOrDefault();
            }

            return item;
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> items = null;

            // extract the dynamic sql query and parameters from predicate
            QueryResult result = DynamicQuery.GetDynamicQuery(_tableName, predicate);

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                items = cn.Query<T>(result.Sql, (object)result.Param);
            }

            return items;
        }

        public virtual IEnumerable<T> FindAll()
        {
            IEnumerable<T> items = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                items = cn.Query<T>("SELECT * FROM " + _tableName);
            }

            return items;
        }

        public bool Execute(string spName, DynamicParameters parameters)
        {
            using (var connection = Connection)
            {
                var result =
                    connection.Query<int>(
                        spName,
                        parameters,
                        commandType: CommandType.StoredProcedure).
                               First();

                return result == 1;
            }
        }

        //public T GetSingle<T>(string spName, KeyValuePair<string, object> parameters)
        //{
        //    using (var connection = GetOpenConnection())
        //    {
        //        var p = GetDynamicParameters(new List<KeyValuePair<string, object>>() { parameters });
        //        var result = connection.Query<T>(spName, p, commandType: CommandType.StoredProcedure);
        //        return result.FirstOrDefault();
        //    }
        //}

        public T GetSingle(string spName, DynamicParameters parameters)
        {
            using (var connection = Connection)
            {
                var result = connection.Query<T>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public IEnumerable<T> GetMultiple(string spName, DynamicParameters parameters)
        {
            using (var connection = Connection)
            {
                var result = connection.Query<T>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
    }
}
