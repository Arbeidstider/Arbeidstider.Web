using System;

namespace Arbeidstider.Business.Interfaces.Caching
{
    public interface ICacheService
    {
        void DeleteAll();
        void Invalidate(string cacheID);
        T Get<T>(string cacheID, Func<T> getItemCallback) where T : class;
    }
}
