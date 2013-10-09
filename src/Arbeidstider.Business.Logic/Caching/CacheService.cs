using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using Arbeidstider.Business.Interfaces.Caching;

namespace Arbeidstider.Business.Logic.Caching
{
    public class CacheService : ICacheService
    {
        private readonly Cache _cache;
        private readonly DateTime _absoluteExpiration;
        private static readonly TimeSpan _slidingExpiration = new TimeSpan(1, 0, 0);

        public CacheService(DateTime absoluteExpiration)
        {
            _absoluteExpiration = absoluteExpiration;
            _cache = HttpContext.Current.Cache;
        }

        public void DeleteAll()
        {
            List<string> toRemove = new List<string>();
            foreach (DictionaryEntry cacheItem in HttpRuntime.Cache) {
                toRemove.Add(cacheItem.Key.ToString());
            }

            foreach (string key in toRemove) {
                HttpRuntime.Cache.Remove(key);
            }
        }

        public void Invalidate(string cacheID)
        {
            List<string> toRemove = new List<string>();
            foreach (DictionaryEntry cacheItem in HttpRuntime.Cache) {
                if (cacheItem.Key.Equals(cacheID))
                    toRemove.Add(cacheItem.Key.ToString());
            }

            foreach (string key in toRemove) {
                HttpRuntime.Cache.Remove(key);
            }
        }

        public T Get<T>(string cacheID, Func<T> getItemCallback) where T : class
        {
            T item = HttpRuntime.Cache.Get(cacheID) as T;
            if (item == null)
            {
                item = getItemCallback();
                HttpContext.Current.Cache.Insert(cacheID, item, null, _absoluteExpiration, TimeSpan.Zero);
            }
            return item;
        }
    }
}