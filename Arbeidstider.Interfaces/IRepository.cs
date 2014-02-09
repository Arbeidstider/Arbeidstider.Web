using System.Collections.Generic;
using Dapper;

namespace Arbeidstider.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll(object parameters);
        T Create(object parameters);
        T Get(object parameters);
        bool Update(object parameters);
        bool Exists(object parameters);
        bool Delete(object parameters);
        DynamicParameters GetParameters(object parameters);
    }
}
