using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Arbeidstider.Business.Interfaces.Database;
using Arbeidstider.Database.Exceptions;
using log4net;

namespace Arbeidstider.Database
{
    public enum DatabaseMode
    {
        Test
    };

    public class DatabaseConnection : IDatabaseConnection
    {
        private readonly ILog Logger;
        protected DatabaseMode _mode;
        protected internal string _connectionString;

        public DatabaseConnection(ILog logger = null, string connectionString = null)
        {
            if (logger == null)
                Logger = LogManager.GetLogger("FileLogger");
            else
                Logger = logger;

            if (connectionString == null)
            {
                if (HttpContext.Current.IsDebuggingEnabled)
                    _connectionString = ConfigurationManager.ConnectionStrings["Debug"].ToString();
                else
                    _connectionString = ConfigurationManager.ConnectionStrings["Release"].ToString();
            }
            else
            {
                if (connectionString == "test")
                    _connectionString = ConfigurationManager.ConnectionStrings["Test"].ToString();
                else
                    _connectionString = connectionString;
            }
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public DataTable ExecuteSP(string spName, KeyValuePair<string, object> parameters)
        {
            return ExecuteSP(spName, new List<KeyValuePair<string, object>>() {parameters});
        }

        public DataTable ExecuteSP(string spName, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var param in parameters)
                    {
                        if (param.Value == null)
                        {
                            Logger.Warn(string.Format("A null parameter was defined for stored procedure: {0} {1}",
                                param.Key, spName));
                            continue;
                        }
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    try
                    {
                        if (_mode == DatabaseMode.Test)
                            tran.Rollback();
                        else
                            tran.Commit();


                        var reader = cmd.ExecuteReader();
                        var dt = new DataTable();
                        dt.Load(reader);
                        conn.Close();
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        string exception = string.Format("Failed to execute stored procedure: {0}\n{1}", spName,
                            ex.Message);
                        Logger.Error(exception);
                        throw new DatabaseException(exception);
                    }
                }
            }
        }
    }
}