using System.Collections.Generic;

namespace Arbeidstider.Business.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll(List<KeyValuePair<string, object>> parameters);
        T Create(List<KeyValuePair<string, object>> parameters);
        bool Update(T obj, List<KeyValuePair<string, object>> parameters);
        bool Verify(List<KeyValuePair<string, object>> parameters);
    }
}
