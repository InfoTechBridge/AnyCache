using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyCache.Core
{
    public interface IAnyHashCache
    {
        Task AddOrUpdateHashAsync(string key, List<KeyValuePair<string, object>> values);
        Task AddOrUpdateHashAsync(string key, string name, object value);
        Task<List<KeyValuePair<string, object>>> GetHashAsync(string key);
        Task<object> GetHashAsync(string key, string name);
        Task<T> GetHashAsync<T>(string key, string name);
        Task<long> IncrementHashAsync(string key, string name, long value);
    }
}
