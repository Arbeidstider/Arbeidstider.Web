using System;
using System.Configuration;
using ServiceStack.Caching;
using ServiceStack.Redis;

namespace Arbeidstider.Web.Framework.Cache
{
    public class CacheClient
    {
        private static ICacheClient GetClient()
        {
            using (var manager = new PooledRedisClientManager(ConfigurationManager.AppSettings["RedisUrl"]))
            {
                return manager.GetCacheClient();
            }
        }

        public static TResult GetFromOrAddToCache<TResult>(string cacheKey, Func<TResult> callback, TimeSpan expiresIn)
        {
            using (var cache = GetClient())
            {
                object obj = cache.Get<TResult>(cacheKey);
                if (obj != null) return (TResult) obj;
                else
                {
                    obj = callback();
                    if (obj != null)
                    {
                        cache.Add(cacheKey, obj, DateTime.Now.AddMinutes(15));
                        return (TResult) obj;
                    }
                }
            }
            return default(TResult);
        }
    }
}
