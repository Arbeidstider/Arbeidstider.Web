using System.Data;
using Arbeidstider.Business.Logic.Enums;

namespace Arbeidstider.Business.Logic.Repository
{
    public static class RepositoryExtensions
    {
        public static bool QueryExecutedSuccessfully(this DataTable dt)
        {
            return (dt.Rows != null  && dt.Rows[0] != null && dt.Rows[0]["Result"] != null &&
                (DatabaseResult) (int) dt.Rows[0]["Result"] == DatabaseResult.FAIL);
        }
    }
}
