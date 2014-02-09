using System.Collections.Generic;
namespace Arbeidstider.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll(object parameters);
        int Create(object parameters);
        T Get(object parameters);
        bool Update(object parameters);
        bool Exists(object parameters);
        bool Delete(object parameters);
    }
}
