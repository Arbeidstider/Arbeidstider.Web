using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Arbeidstider.Interfaces;
using Dapper;

namespace Arbeidstider.DataAccess
{
    public class Database : IDatabase
    {
        private static IDatabase _instance = null;

        public static IDatabase Instance
        {
            get { return _instance ?? (_instance = new Database()); }
        }

        #region Database Connection
        private static string ConnectionString()
        {
            string connectionString;
            if (HttpContext.Current.IsDebuggingEnabled)
                connectionString = ConfigurationManager.ConnectionStrings["Debug"].ToString();
            else
                connectionString = ConfigurationManager.ConnectionStrings["Release"].ToString();
            return connectionString;
        }

        private static SqlConnection GetOpenConnection()
        {
            using (var connection = new SqlConnection(ConnectionString()))
            {
                connection.Open();
                return connection;
            }
        }

        #endregion

        

        //private static DynamicParameters GetDynamicParameters(IEnumerable<KeyValuePair<string, object>> parameters)
        //{
        //    var p = new DynamicParameters();
        //    foreach (var kvp in parameters)
        //    {
        //        DbType dbType;
        //        object value;
        //        if (kvp.Value is int)
        //        {
        //            dbType = DbType.Int32;
        //            value = int.Parse(kvp.Value.ToString());
        //        }
        //        else if (kvp.Value is DateTime)
        //        {
        //            dbType = DbType.DateTime;
        //            value = DateTime.Parse(kvp.Value.ToString());
        //        }
        //        else
        //        {
        //            dbType = DbType.String;
        //            value = kvp.Value.ToString();
        //        }

        //        p.Add(name: kvp.Key, value: value, dbType: dbType, direction: ParameterDirection.Input);
        //    }

        //    return p;
        //}

        public bool Execute(string spName, DynamicParameters parameters)
        {
            using (var connection = GetOpenConnection())
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

        public T GetSingle<T>(string spName, DynamicParameters parameters)
        {
            using (var connection = GetOpenConnection())
            {
                var result = connection.Query<T>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public IEnumerable<T> GetMultiple<T>(string spName, DynamicParameters parameters)
        {
            using (var connection = GetOpenConnection())
            {
                var result = connection.Query<T>(spName, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
    }
}