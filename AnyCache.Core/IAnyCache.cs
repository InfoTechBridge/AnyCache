using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnyCache.Core
{
    public interface IAnyCache : IEnumerable<KeyValuePair<string, object>>, IEnumerable
    {
        object this[string key] { get; set; }

        bool Add(string key, object value, DateTimeOffset? absoluteExpiration = null);
        bool Add<T>(string key, T value, DateTimeOffset? absoluteExpiration = null);
        bool Add(string key, object value, TimeSpan slidingExpiration);        
        bool Add<T>(string key, T value, TimeSpan slidingExpiration);
        
        

        void Set(string key, object value, DateTimeOffset? absoluteExpiration = null);
        void Set<T>(string key, T value, DateTimeOffset? absoluteExpiration = null);
        void Set(string key, object value, TimeSpan slidingExpiration);
        void Set<T>(string key, T value, TimeSpan slidingExpiration);

        bool Contains(string key);

        object Get(string key);
        T Get<T>(string key);

        //T GetValueOrDefault<T>(string key);
        T GetValueOrDefault<T>(string key, T value);
        //bool TryGetValue(string key, out object result);
        //bool TryGetValue<T>(string key, out T result);

        Task<object> GetAsync(string key);
        Task<T> GetAsync<T>(string key);

        object GetValueOrAdd(string key, object value, DateTimeOffset? absoluteExpiration = null);
        T GetValueOrAdd<T>(string key, T value, DateTimeOffset? absoluteExpiration = null);
        object GetValueOrAdd(string key, object value, TimeSpan slidingExpiration);
        T GetValueOrAdd<T>(string key, T value, TimeSpan slidingExpiration);

        object GetValueOrAdd(string key, Func<object> retriever, DateTimeOffset? absoluteExpiration = null);
        T GetValueOrAdd<T>(string key, Func<T> retriever, DateTimeOffset? absoluteExpiration = null);
        object GetValueOrAdd(string key, Func<object> retriever, TimeSpan slidingExpiration);
        T GetValueOrAdd<T>(string key, Func<T> retriever, TimeSpan slidingExpiration);

        IDictionary<string, object> GetAll(IEnumerable<string> keys);
        IDictionary<string, T> GetAll<T>(IEnumerable<string> keys);

        object Remove(string key);
        T Remove<T>(string key);

        long GetCount();

        //IEnumerator<KeyValuePair<string, object>> GetEnumerator();

        
        void ClearCache();
        void Compact();
    }
}
