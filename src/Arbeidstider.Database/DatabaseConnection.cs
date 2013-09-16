using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Arbeidstider.Database
{
    public class DatabaseConnection
    {
        private readonly static string _connectionString =
            @"Data Source=remote.bjonn.com,4200\SQLSERVER;Initial Catalog=Arbeidstider;User ID=dbUserArbeidstider;Password=kottbullar2013";

        private static DatabaseConnection _instance = null;
        private static SqlConnection _connection = null;

        public static DatabaseConnection Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DatabaseConnection();

                return _instance;
            }
        }


        private DatabaseConnection()
        {
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
