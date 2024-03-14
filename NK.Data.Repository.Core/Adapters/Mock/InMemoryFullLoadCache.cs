using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace NK.Data.Repository.Core.Adapters.Mock
{
    public static class RepositoryCacheDefaultExpiration
    {
        public static int InMemoryFullLoadCacheDefaultExpirationTimeInMinute = 43200; // 30 Days in minutes
        public static int InMemoryQueryLoadCacheDefaultExpirationTimeInMinute = 60; // One hour

    }

    /// <summary>
    /// This class loads ALL entities of its table when it's started.
    /// When reading, it returns straight from cache,
    /// When updated, it updates the cache AND the database,
    /// when inserting, it inserts to the cache AND the database,
    /// when deleting, it deletes from cache AND the database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// 

    public static class InMemoryFullLoadCacheKey<T>
    {
        public static string GetTypeKey(int? shopId = 0, string includeProperties = null)
        {
            string incProp = includeProperties ?? "noIncludes";
            return $"{typeof(T).FullName}_{shopId}_{incProp.Trim()}_fullLoad";
        }
    }

    public class InMemoryFullLoadCache<T>
    {
        private readonly ObjectCache _cache;
        private readonly CacheItemPolicy _policy;
        private readonly int _shopId;
        private readonly string _includeProperties;

        public InMemoryFullLoadCache(int expirationTimeInMinute = 0, int? shopId = 0, string includeProperties = null)
        {
            _cache = MemoryCache.Default;

            //This works on the whole context, which might be very wrong. It should probably work on IEnumerable<object>
            if (expirationTimeInMinute <= 0)
                expirationTimeInMinute = RepositoryCacheDefaultExpiration.InMemoryFullLoadCacheDefaultExpirationTimeInMinute;

            _policy = new CacheItemPolicy
            {
                AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddMinutes(expirationTimeInMinute))
            };

            _shopId = shopId.GetValueOrDefault(0);
            _includeProperties = includeProperties ?? "noIncludes";
        }

        private string Type => InMemoryFullLoadCacheKey<T>.GetTypeKey(_shopId, _includeProperties);
        public IEnumerable<T> Entities => _cache[Type] as IEnumerable<T>;

        public bool IsInitialized()
        {
            return _cache[Type] != null;
        }

        public void Initialize(IEnumerable<T> elements)
        {
            _cache.Set(Type, elements, _policy);
        }

        public void Invalidate()
        {
            if (!IsInitialized())
                return;

            if (_cache.Contains(Type))
                _cache.Remove(Type);
        }
    }
}
