using System;

namespace Arbeidstider.Business.Interfaces.Caching
{
    public interface ICacheService
    {
        T Get<T>(string cacheID, Func<T> getItemCallback) where T : class;
    }
}
