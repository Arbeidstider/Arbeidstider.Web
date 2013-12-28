using System.Data;

namespace Arbeidstider.DataAccess.Repository
{
    public static class RepositoryExtensions
    {
        public static bool QueryExecutedSuccessfully(this DataTable dt)
        {
            return (dt.Rows != null  && dt.Rows.Count > 0 && dt.Rows[0] != null && dt.Rows[0]["Result"] != null &&
                 (int) dt.Rows[0]["Result"] == 1);
        }
    }
}
