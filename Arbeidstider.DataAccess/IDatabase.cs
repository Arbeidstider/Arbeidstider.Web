using System.Collections.Generic;

namespace Arbeidstider.DataAccess
{
    public interface IDatabase
    {
        bool Execute(string spName, KeyValuePair<string, object> parameters);
        bool Execute(string spName, IEnumerable<KeyValuePair<string, object>> parameters);
        T GetSingle<T>(string spName, KeyValuePair<string, object> parameters);
        T GetSingle<T>(string spName, IEnumerable<KeyValuePair<string, object>> parameters);
        IEnumerable<T> GetMultiple<T>(string spName, KeyValuePair<string, object> parameters);
        IEnumerable<T> GetMultiple<T>(string spName, IEnumerable<KeyValuePair<string, object>> parameters);
    }
}