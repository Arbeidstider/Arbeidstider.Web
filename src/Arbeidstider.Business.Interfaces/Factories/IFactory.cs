using System.Data;

namespace Arbeidstider.Business.Interfaces.Factories
{
    public interface IFactory<T>
    {
        T Create(DataTable dt);
    }
}
