using System.Collections.Generic;

namespace Arbeidstider.Business.Interfaces.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll(List<KeyValuePair<string, object>> parameters);
        T Create(List<KeyValuePair<string, object>> parameters);
        T Get(IEnumerable<KeyValuePair<string, object>> parameters);
        bool Update(List<KeyValuePair<string, object>> parameters);
        bool Exists(List<KeyValuePair<string, object>> parameters);
    }
}
