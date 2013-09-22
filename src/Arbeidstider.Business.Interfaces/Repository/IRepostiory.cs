using System.Collections.Generic;

namespace Arbeidstider.Business.Interfaces.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll(List<KeyValuePair<string, object>> parameters);
        T Create(List<KeyValuePair<string, object>> parameters);
        T Get(KeyValuePair<string, object> parameters);
        bool Update(T obj, List<KeyValuePair<string, object>> parameters);
        bool Verify(List<KeyValuePair<string, object>> parameters);
    }
}
