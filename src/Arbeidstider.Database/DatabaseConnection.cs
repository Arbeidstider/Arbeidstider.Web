using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Arbeidstider.Business.Interfaces.Database;
using Arbeidstider.Database.Exceptions;
using log4net;

namespace Arbeidstider.Database
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private readonly string _connectionString;
        private readonly ILog Logger;

        public DatabaseConnection(string connectionString, ILog _logger)
        {
            _connectionString = connectionString;
            Logger = _logger;
        }

        public DataTable ExecuteSP(string spName, KeyValuePair<string, object> parameters)
        {
            return ExecuteSP(spName, new List<KeyValuePair<string, object>>() {parameters});
        }

        public DataTable ExecuteSP(string spName, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            if (parameters == null) return new DataTable();
            using (SqlConnection conn = new SqlConnection(_connectionString)) 
            {
                conn.Open();
                DataTable dt = new DataTable();
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    try
                    {
                        var reader = cmd.ExecuteReader();
                        dt.Load(reader);
                    }
                    catch (Exception ex)
                    {
                       string exception = string.Format("Failed to execute stored procedure: {0}\n{1}", spName, ex.Message);
                       Logger.Error(exception);
                       throw new DatabaseException(exception); 
                    }
                }
                conn.Close();
                return dt;
            }
        }
    }
}