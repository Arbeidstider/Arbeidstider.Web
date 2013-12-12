using Arbeidstider.Cache;

namespace Arbeidstider.Web.Framework
{
    public class Cache
    {
        private static readonly ICacheService _cacheService = IoC.Resolve<ICacheService>();

        public static void Flush()
        {
            _cacheService.DeleteAll();
        }
    }
}
