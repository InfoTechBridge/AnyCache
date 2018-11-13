using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyCache.Core
{
    public abstract class CacheBase : IAnyCache
    {
        public object this[string key] { get => Get(key); set => Set(key, value); }

        public abstract bool Add(string key, object value, DateTimeOffset? absoluteExpiration = null);

        public abstract bool Add<T>(string key, T value, DateTimeOffset? absoluteExpiration = null);

        public abstract bool Add(string key, object value, TimeSpan slidingExpiration);

        public abstract bool Add<T>(string key, T value, TimeSpan slidingExpiration);

        

        public abstract void Set(string key, object value, DateTimeOffset? absoluteExpiration = null);

        public abstract void Set<T>(string key, T value, DateTimeOffset? absoluteExpiration = null);

        public abstract void Set(string key, object value, TimeSpan slidingExpiration);

        public abstract void Set<T>(string key, T value, TimeSpan slidingExpiration);

        public abstract bool Contains(string key);

        public abstract object Get(string key);

        public abstract T Get<T>(string key);

        public virtual T GetValueOrDefault<T>(string key, T value)
        {
            var val = Get<T>(key);
            if (val != null)
                return val;
            else
                return value;
        }

        public virtual async Task<object> GetAsync(string key)
        {
            return await Task.Run<object>(() =>
            {
                return Get(key);
            });

            //var val = await _db.StringGetAsync(ToRedisKey(key));
            //if (!val.IsNull)
            //    return FromRedisValue(val);
            //else
            //    return null;

            //TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            //return tcs.Task;
        }

        public virtual async Task<T> GetAsync<T>(string key)
        {
            return await Task.Run<T>(() =>
            {
                return Get<T>(key);
            });

            //var val = await _db.StringGetAsync(ToRedisKey(key));
            //if (!val.IsNull)
            //    return FromRedisValue<T>(val);
            //else
            //    return default(T);
        }

        public virtual object GetValueOrAdd(string key, object value, DateTimeOffset? absoluteExpiration = null)
        {
            var val = Get(key);
            if (val != null)
                return val;
            else
            {
                Set(key, value, absoluteExpiration);
                return value;
            }
        }

        public virtual T GetValueOrAdd<T>(string key, T value, DateTimeOffset? absoluteExpiration = null)
        {
            var val = Get<T>(key);
            if (val != null)
                return val;
            else
            {
                Set<T>(key, value, absoluteExpiration);
                return value;
            }
        }

        public virtual object GetValueOrAdd(string key, object value, TimeSpan slidingExpiration)
        {
            var val = Get(key);
            if (val != null)
                return val;
            else
            {
                Set(key, value, slidingExpiration);
                return value;
            }
        }

        public virtual T GetValueOrAdd<T>(string key, T value, TimeSpan slidingExpiration)
        {
            var val = Get<T>(key);
            if (val != null)
                return val;
            else
            {
                Set<T>(key, value, slidingExpiration);
                return value;
            }
        }

        public virtual object GetValueOrAdd(string key, Func<object> retriever, DateTimeOffset? absoluteExpiration = null)
        {
            var val = Get(key);
            if (val != null)
                return val;
            else
            {
                var value = retriever?.Invoke();
                if (value != null)
                    Set(key, value, absoluteExpiration);

                return value;
            }
        }

        public virtual T GetValueOrAdd<T>(string key, Func<T> retriever, DateTimeOffset? absoluteExpiration = null)
        {
            var val = Get<T>(key);
            if (val != null)
                return val;
            else
            {
                var value = retriever.Invoke();
                if (value != null)
                    Set<T>(key, value, absoluteExpiration);

                return value;
            }
        }

        public virtual object GetValueOrAdd(string key, Func<object> retriever, TimeSpan slidingExpiration)
        {
            var val = Get(key);
            if (val != null)
                return val;
            else
            {
                var value = retriever?.Invoke();
                if (value != null)
                    Set(key, value, slidingExpiration);

                return value;
            }
        }

        public virtual T GetValueOrAdd<T>(string key, Func<T> retriever, TimeSpan slidingExpiration)
        {
            var val = Get<T>(key);
            if (val != null)
                return val;
            else
            {
                var value = retriever.Invoke();
                if (value != null)
                    Set<T>(key, value, slidingExpiration);

                return value;
            }
        }

        public abstract IDictionary<string, object> GetAll(IEnumerable<string> keys);

        public abstract IDictionary<string, T> GetAll<T>(IEnumerable<string> keys);

        public abstract object Remove(string key);

        public abstract T Remove<T>(string key);

        public abstract long GetCount();

        public abstract IEnumerator<KeyValuePair<string, object>> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract void ClearCache();

        public abstract void Compact();

    }
}
