using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Arbeidstider.Database.Constants;

namespace Arbeidstider.Database
{
    public class DatabaseConnection
    {
        private readonly string _connectionString;
        private static DatabaseConnection _instance;

        public static DatabaseConnection Instance
        {
            get
            {
                if (_instance == null)
                    if (HttpContext.Current.Request.Url.ToString().Contains("arbeidstider.no"))
                        _instance = new DatabaseConnection(ConnectionStrings.LOCAL);
                    _instance = new DatabaseConnection(ConnectionStrings.REMOTE);


                return _instance;
            }
        }

        private DatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
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