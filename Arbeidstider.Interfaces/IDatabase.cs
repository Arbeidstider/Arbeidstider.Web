using System.Collections.Generic;
using Dapper;

namespace Arbeidstider.Interfaces
{
    public interface IDatabase
    {
        bool Execute(string spName, DynamicParameters parameters);
        T GetSingle<T>(string spName, DynamicParameters parameters);
        IEnumerable<T> GetMultiple<T>(string spName, DynamicParameters parameters);
    }
}