using System.Collections.Generic;

namespace Arbeidstider.Business.Interfaces.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll(IEnumerable<KeyValuePair<string, object>> parameters);
        bool Create(IEnumerable<KeyValuePair<string, object>> parameters);
        T Get(IEnumerable<KeyValuePair<string, object>> parameters);
        bool Update(IEnumerable<KeyValuePair<string, object>> parameters);
        bool Exists(IEnumerable<KeyValuePair<string, object>> parameters);
    }
}
