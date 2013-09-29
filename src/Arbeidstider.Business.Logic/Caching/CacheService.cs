using System;
using System.Web;
using Arbeidstider.Business.Interfaces.Caching;

namespace Arbeidstider.Business.Logic.Caching
{
    public class CacheService : ICacheService
    {
        private static readonly TimeSpan _slidingExpiration = new TimeSpan(0, 0, 60);

        public T Get<T>(string cacheID, Func<T> getItemCallback, DateTime expires) where T : class
        {
            T item = HttpRuntime.Cache.Get(cacheID) as T;
            if (item == null)
            {
                item = getItemCallback();
                HttpContext.Current.Cache.Insert(cacheID, item, null, expires, _slidingExpiration);
            }
            return item;
        }
    }
}
