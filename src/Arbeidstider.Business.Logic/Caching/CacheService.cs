using System;
using System.Web;
using Arbeidstider.Business.Interfaces.Caching;

namespace Arbeidstider.Business.Logic.Caching
{
    public class CacheService : ICacheService
    {
        public T Get<T>(string cacheID, Func<T> getItemCallback, DateTime expires) where T : class
        {
            T item = HttpRuntime.Cache.Get(cacheID) as T;
            if (item == null)
            {
                item = getItemCallback();
                HttpContext.Current.Cache.Insert(cacheID, item, null, expires, new TimeSpan(0, 0, 15));
            }
            return item;
        }
    }
}
