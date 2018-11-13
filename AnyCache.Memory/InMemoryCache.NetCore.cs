#if NETSTANDARD
using AnyCache.Core;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnyCache.InMemory
{
    public class InMemoryCache : CacheBase, IDisposable
    {
        private readonly MemoryCache _cache;

        public InMemoryCache()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        public InMemoryCache(MemoryCache cache)
        {
            _cache = cache;
        }

        //public object this[string key] { get => Get(key); set => Set(key, value); }

        public override bool Add(string key, object value, DateTimeOffset? absoluteExpiration = null)
        {
            var entry = _cache.CreateEntry(key);
            entry.AbsoluteExpiration = absoluteExpiration;
            entry.SetValue(value);
            entry.Dispose();

            return true;
        }
        public override bool Add<T>(string key, T value, DateTimeOffset? absoluteExpiration = null)
        {
            var entry = _cache.CreateEntry(key);
            entry.AbsoluteExpiration = absoluteExpiration;
            entry.SetValue(value);
            entry.Dispose();

            return true;
        }
        public override bool Add(string key, object value, TimeSpan slidingExpiration)
        {
            var entry = _cache.CreateEntry(key);
            entry.AbsoluteExpirationRelativeToNow = slidingExpiration;
            entry.SetValue(value);
            entry.Dispose();

            return true;
        }
        public override bool Add<T>(string key, T value, TimeSpan slidingExpiration)
        {
            var entry = _cache.CreateEntry(key);
            entry.AbsoluteExpirationRelativeToNow = slidingExpiration;
            entry.SetValue(value);
            entry.Dispose();

            return true;
        }

        public override object GetValueOrAdd(string key, object value, DateTimeOffset? absoluteExpiration = null)
        {
            return _cache.GetOrCreate(key, entry =>
            {
                entry.AbsoluteExpiration = absoluteExpiration;
                return value;
            });
        }
        public override T GetValueOrAdd<T>(string key, T value, DateTimeOffset? absoluteExpiration = null)
        {
            return _cache.GetOrCreate<T>(key, entry =>
            {
                entry.AbsoluteExpiration = absoluteExpiration;
                return value;
            });
        }
        public override object GetValueOrAdd(string key, object value, TimeSpan slidingExpiration)
        {
            return _cache.GetOrCreate(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = slidingExpiration;
                return value;
            });
        }
        public override T GetValueOrAdd<T>(string key, T value, TimeSpan slidingExpiration)
        {
            return _cache.GetOrCreate<T>(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = slidingExpiration;
                return value;
            });
        }

        public override object GetValueOrAdd(string key, Func<object> retriever, DateTimeOffset? absoluteExpiration = null)
        {
            return _cache.GetOrCreate(key, entry =>
            {
                var value = retriever?.Invoke();
                entry.AbsoluteExpiration = absoluteExpiration;
                return value;
            });
        }

        public override T GetValueOrAdd<T>(string key, Func<T> retriever, DateTimeOffset? absoluteExpiration = null)
        {
            return _cache.GetOrCreate<T>(key, entry =>
            {
                var value = retriever.Invoke();
                entry.AbsoluteExpiration = absoluteExpiration;
                return value;
            });
        }

        public override object GetValueOrAdd(string key, Func<object> retriever, TimeSpan slidingExpiration)
        {
            return _cache.GetOrCreate(key, entry =>
            {
                var value = retriever?.Invoke();
                entry.AbsoluteExpirationRelativeToNow = slidingExpiration;
                return value;
            });
        }

        public override T GetValueOrAdd<T>(string key, Func<T> retriever, TimeSpan slidingExpiration)
        {
            return _cache.GetOrCreate<T>(key, entry =>
            {
                var value = retriever.Invoke();
                entry.AbsoluteExpirationRelativeToNow = slidingExpiration;                
                return value;
            });
        }

        public override void Set(string key, object value, DateTimeOffset? absoluteExpiration = null)
        {
            if(absoluteExpiration.HasValue)
                _cache.Set(key, value, absoluteExpiration.Value);
            else
                _cache.Set(key, value);
        }
        public override void Set<T>(string key, T value, DateTimeOffset? absoluteExpiration = null)
        {
            if (absoluteExpiration.HasValue)
                _cache.Set<T>(key, value, absoluteExpiration.Value);
            else
                _cache.Set<T>(key, value);
        }
        public override void Set(string key, object value, TimeSpan slidingExpiration)
        {
            _cache.Set(key, value, slidingExpiration);
        }
        public override void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            _cache.Set<T>(key, value, slidingExpiration);
        }

        public override bool Contains(string key)
        {
            object value;
            return _cache.TryGetValue(key, out value);
        }

        public override object Get(string key)
        {
            return _cache.Get(key);
        }
        public override T Get<T>(string key)
        {
            return _cache.Get<T>(key);
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
        //        return _cache.Get<T>(key);
        //    });
        //}        

        public override IDictionary<string, object> GetAll(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }
        public override IDictionary<string, T> GetAll<T>(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        public override object Remove(string key)
        {
            var val = Get(key);
            if (val != null)
            {
                _cache.Remove(key);
                return val;
            }
            else
                return null;
        }
        public override T Remove<T>(string key)
        {
            var val = Get<T>(key);
            if (val != null)
            {
                _cache.Remove(key);
                return val;
            }
            else
                return default(T);
        }

        public override long GetCount()
        {
            return _cache.Count;
        }

        public override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}

        public override void ClearCache()
        {
            throw new NotImplementedException();
        }

        public override void Compact()
        {
            _cache.Compact(100);
        }

        public void Dispose()
        {
            
        }
    }
}

#endif
