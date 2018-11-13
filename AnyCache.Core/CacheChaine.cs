using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCache.Core
{
    public class CacheChaine : IAnyCache
    {
        private readonly List<IAnyCache> _caches;

        public CacheChaine(IEnumerable<IAnyCache> caches)
        {
            _caches = caches.ToList();
        }

        public object this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Add(string key, object value, DateTimeOffset? absoluteExpiration = null)
        {
            throw new NotImplementedException();
        }

        public bool Add<T>(string key, T value, DateTimeOffset? absoluteExpiration = null)
        {
            throw new NotImplementedException();
        }

        public bool Add(string key, object value, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public bool Add<T>(string key, T value, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public object GetValueOrAdd(string key, object value, DateTimeOffset? absoluteExpiration = null)
        {
            throw new NotImplementedException();
        }

        public T GetValueOrAdd<T>(string key, T value, DateTimeOffset? absoluteExpiration = null)
        {
            throw new NotImplementedException();
        }

        public object GetValueOrAdd(string key, object value, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public T GetValueOrAdd<T>(string key, T value, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public object GetValueOrAdd(string key, Func<object> retriever, DateTimeOffset? absoluteExpiration = null)
        {
            throw new NotImplementedException();
        }

        public T GetValueOrAdd<T>(string key, Func<T> retriever, DateTimeOffset? absoluteExpiration = null)
        {
            throw new NotImplementedException();
        }

        public object GetValueOrAdd(string key, Func<object> retriever, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public T GetValueOrAdd<T>(string key, Func<T> retriever, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public void ClearCache()
        {
            throw new NotImplementedException();
        }

        public void Compact()
        {
            throw new NotImplementedException();
        }

        public bool Contains(string key)
        {
            throw new NotImplementedException();
        }

        public object Get(string key)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            IAnyCache upperCache = null;
            foreach (var cache in _caches)
            {
                var val = cache.Get<T>(key);

                if (val != null)
                {
                    if (upperCache != null)
                        upperCache.Set<T>(key, val);

                    return val;
                }

                upperCache = cache;
            }

            return default(T);
        }

        public IDictionary<string, object> GetAll(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, T> GetAll<T>(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public long GetCount()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public object Remove(string key)
        {
            throw new NotImplementedException();
        }

        public T Remove<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, object value, DateTimeOffset? absoluteExpiration = null)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, T value, DateTimeOffset? absoluteExpiration = null)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, object value, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public T GetValueOrDefault<T>(string key, T value)
        {
            throw new NotImplementedException();
        }
    }
}
