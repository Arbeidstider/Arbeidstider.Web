using Arbeidstider.DataAccess.Domain;
using Arbeidstider.Interfaces;
using ServiceStack.Redis;

namespace Arbeidstider.Web.Framework.Cache
{
    public class EmployeeCache : CacheBase, ICache<IEmployee>
    {
        protected EmployeeCache(IRedisClient client) : base(client)
        {
        }

        public IEmployee GetById(int Id)
        {
            return new Employee();
        }
    }

    public interface ICache<T>
    {
    }
}
