using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Arbeidstider.Business.Interfaces.Database;
using Arbeidstider.Database.Constants;

namespace Arbeidstider.Database
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private readonly string _connectionString;
        private static DatabaseConnection _instance;

        public static DatabaseConnection Instance
        {
            get
            {
                if (_instance == null)
                    if (!HttpContext.Current.IsDebuggingEnabled)
                        _instance = new DatabaseConnection(ConnectionStrings.RELEASE);
                    _instance = new DatabaseConnection(ConnectionStrings.DEBUG);


                return _instance;
            }
        }

        public DatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
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
                DataTable dt = new DataTable();
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    var reader = cmd.ExecuteReader();

                    dt.Load(reader);

                }
                conn.Close();
                return dt;
            }
        }
    }
}