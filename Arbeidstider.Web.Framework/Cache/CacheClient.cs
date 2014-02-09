using System.Configuration;
using ServiceStack.Caching;
using ServiceStack.Redis;

namespace Arbeidstider.Web.Framework.Cache
{
    public class CacheClient
    {
        public static ICacheClient GetClient()
        {
            using (var manager = new PooledRedisClientManager(ConfigurationManager.AppSettings["RedisUrl"]))
            {
                return manager.GetCacheClient();
            }
        }
    }
}
