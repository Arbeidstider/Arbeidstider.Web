using System;
using System.Configuration;
using System.Web;
using ServiceStack.Caching;
using ServiceStack.Redis;

namespace Arbeidstider.Web.Framework.Cache
{
    public class CacheClient
    {
        private readonly static bool _enableCache = false;
        static CacheClient()
        {
            if (!HttpContext.Current.IsDebuggingEnabled)
                _enableCache = true;
        }

        private static ICacheClient GetClient()
        {
            using (var manager = new PooledRedisClientManager(ConfigurationManager.AppSettings["RedisUrl"]))
            {
                return manager.GetCacheClient();
            }
        }

        public static T GetFromOrAddToCache<T>(string cacheKey, Func<T> callback, TimeSpan expiresIn)
        {
            if (!_enableCache) return callback();

            object obj = GetFromCache<T>(cacheKey); 
            if (obj != null) return (T)obj;

            return AddToCache<T>(cacheKey, callback, expiresIn);
        }

        private static T GetFromCache<T>(string cacheKey)
        {
            using (var cache = GetClient())
                return cache.Get<T>(cacheKey);
        }

        private static T AddToCache<T>(string cacheKey, Func<T> callback, TimeSpan expiresIn)
        {
            object obj = callback();
            if (obj != null)
            {
                using (var cache = GetClient())
                    cache.Add(cacheKey, obj, expiresIn);

                return (T)obj;
            }

            return default(T);
        }
    }
}
