using ServiceStack.Redis;

namespace Arbeidstider.Web.Framework.Cache
{
    public class CacheBase
    {
        private readonly IRedisClient _client;
        protected CacheBase(IRedisClient client)
        {
            _client = client;
        }
    }
}
