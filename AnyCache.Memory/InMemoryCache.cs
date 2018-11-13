#if NETFULL
using AnyCache.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace AnyCache.InMemory
{
    public class InMemoryCache : CacheBase, IDisposable
    {
        private readonly ObjectCache _cache;

        public InMemoryCache()
        {
            _cache = MemoryCache.Default;
        }

        public InMemoryCache(ObjectCache cache)
        {
            _cache = cache;
        }

        //public object this[string key] { get => Get(key); set => Set(key, value); }

        public override bool Add(string key, object value, DateTimeOffset? absoluteExpiration = null)
        {
            return _cache.Add(key, value, absoluteExpiration.GetValueOrDefault(DateTimeOffset.MaxValue));
        }
        public override bool Add<T>(string key, T value, DateTimeOffset? absoluteExpiration = null)
        {
            return _cache.Add(key, value, absoluteExpiration.GetValueOrDefault(DateTimeOffset.MaxValue));
        }
        public override bool Add(string key, object value, TimeSpan slidingExpiration)
        {
            return _cache.Add(key, value, new CacheItemPolicy { SlidingExpiration = slidingExpiration });
        }
        public override bool Add<T>(string key, T value, TimeSpan slidingExpiration)
        {
            return _cache.Add(key, value, new CacheItemPolicy { SlidingExpiration = slidingExpiration });
        }

        public override object GetValueOrAdd(string key, object value, DateTimeOffset? absoluteExpiration = null)
        {
            return _cache.AddOrGetExisting(key, value, absoluteExpiration.GetValueOrDefault(DateTimeOffset.MaxValue));
        }
        public override T GetValueOrAdd<T>(string key, T value, DateTimeOffset? absoluteExpiration = null)
        {
            return (T)_cache.AddOrGetExisting(key, value, absoluteExpiration.GetValueOrDefault(DateTimeOffset.MaxValue));
        }
        public override object GetValueOrAdd(string key, object value, TimeSpan slidingExpiration)
        {
            return _cache.AddOrGetExisting(key, value, new CacheItemPolicy { SlidingExpiration = slidingExpiration });
        }
        public override T GetValueOrAdd<T>(string key, T value, TimeSpan slidingExpiration)
        {
            return (T)_cache.AddOrGetExisting(key, value, new CacheItemPolicy { SlidingExpiration = slidingExpiration });
        }

        public override object GetValueOrAdd(string key, Func<object> retriever, DateTimeOffset? absoluteExpiration = null)
        {
            return _cache.AddOrGetExisting(key, retriever?.Invoke(), absoluteExpiration.GetValueOrDefault(DateTimeOffset.MaxValue));
        }

        public override T GetValueOrAdd<T>(string key, Func<T> retriever, DateTimeOffset? absoluteExpiration = null)
        {
            return (T)_cache.AddOrGetExisting(key, retriever.Invoke(), absoluteExpiration.GetValueOrDefault(DateTimeOffset.MaxValue));
        }

        public override object GetValueOrAdd(string key, Func<object> retriever, TimeSpan slidingExpiration)
        {
            return _cache.AddOrGetExisting(key, retriever?.Invoke(), new CacheItemPolicy { SlidingExpiration = slidingExpiration });
        }

        public override T GetValueOrAdd<T>(string key, Func<T> retriever, TimeSpan slidingExpiration)
        {
            return (T)_cache.AddOrGetExisting(key, retriever.Invoke(), new CacheItemPolicy { SlidingExpiration = slidingExpiration });
        }

        public override void Set(string key, object value, DateTimeOffset? absoluteExpiration = null)
        {
            _cache.Set(key, value, absoluteExpiration.GetValueOrDefault(DateTimeOffset.MaxValue));
        }
        public override void Set<T>(string key, T value, DateTimeOffset? absoluteExpiration = null)
        {
            _cache.Set(key, value, absoluteExpiration.GetValueOrDefault(DateTimeOffset.MaxValue));
        }
        public override void Set(string key, object value, TimeSpan slidingExpiration)
        {
            _cache.Set(key, value, new CacheItemPolicy { SlidingExpiration = slidingExpiration });
        }
        public override void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            _cache.Set(key, value, new CacheItemPolicy { SlidingExpiration = slidingExpiration });
        }

        public override bool Contains(string key)
        {
            return _cache.Contains(key);
        }

        public override object Get(string key)
        {
            return _cache.Get(key);
        }
        public override T Get<T>(string key)
        {
            return (T)_cache.Get(key);
        }

        //public async Task<object> GetAsync(string key)
        //{
        //    return await Task.Run(() =>
        //    {
        //        return _cache.Get(key);
        //    });
        //}

        //public async Task<T> GetAsync<T>(string key)
        //{
        //    return await Task.Run(() =>
        //    {
        //        return (T)_cache.Get(key);
        //    });
        //}

        public override IDictionary<string, object> GetAll(IEnumerable<string> keys)
        {
            return _cache.GetValues(keys);
        }
        public override IDictionary<string, T> GetAll<T>(IEnumerable<string> keys)
        {
            return (IDictionary<string, T>)_cache.GetValues(keys).Select(i => new KeyValuePair<string, T>(i.Key, (T)i.Value));
        }

        public override object Remove(string key)
        {
            return _cache.Remove(key);
        }
        public override T Remove<T>(string key)
        {
            return (T)_cache.Remove(key);
        }

        public override long GetCount()
        {
            return _cache.GetCount();
        }

        public override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _cache.AsEnumerable().GetEnumerator();
        }

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}

        public override void ClearCache()
        {
            throw new NotImplementedException();
        }

        public override void Compact()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }
    }
}

#endif
