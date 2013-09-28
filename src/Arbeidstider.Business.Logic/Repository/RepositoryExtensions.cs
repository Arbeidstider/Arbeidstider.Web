using System.Data;

namespace Arbeidstider.Business.Logic.Repository
{
    public static class RepositoryExtensions
    {
        public static bool QueryExecutedSuccessfully(this DataTable dt)
        {
            return true;
            //return ((DatabaseResult) (int) dt.Rows[0]["Result"] == DatabaseResult.FAIL || dt.Rows[0] == null);
        }
    }
}
