using System.Collections.Generic;
using System.Data;

namespace Arbeidstider.Business.Interfaces.Database
{
    public interface IDatabaseConnection
    {
        DataTable ExecuteSP(string spName, KeyValuePair<string, object> parameters);
        DataTable ExecuteSP(string spName, IEnumerable<KeyValuePair<string, object>> parameters);
    }
}
