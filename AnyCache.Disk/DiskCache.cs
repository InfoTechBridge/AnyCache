using AnyCache.Core;
using AnyCache.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnyCache.Disk
{
    public class DiskCache : CacheBase
    {
        public readonly string BasePath;
        public readonly string KeyPrefix;
        private readonly ISerializer _serializer;

        public DiskCache(string basePath = null, string keyPrefix = null, ISerializer serializer = null)
        {
            if (string.IsNullOrWhiteSpace(basePath))
                BasePath = "/";
            else
                BasePath = basePath;

            KeyPrefix = keyPrefix;

            if (serializer == null)
                _serializer = new JsonSerializer();
            else
                _serializer = serializer;
        }
                
        public override bool Add(string key, object value, DateTimeOffset? absoluteExpiration = null)
        {
            throw new NotImplementedException();
        }

        public override bool Add<T>(string key, T value, DateTimeOffset? absoluteExpiration = null)
        {
            throw new NotImplementedException();
        }

        public override bool Add(string key, object value, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public override bool Add<T>(string key, T value, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }
                

        public override void ClearCache()
        {
            throw new NotImplementedException();
        }

        public override void Compact()
        {
            throw new NotImplementedException();
        }

        public override bool Contains(string key)
        {
            throw new NotImplementedException();
        }

        public override object Get(string key)
        {
            throw new NotImplementedException();
        }

        public override T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public override IDictionary<string, object> GetAll(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        public override IDictionary<string, T> GetAll<T>(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        public override long GetCount()
        {
            throw new NotImplementedException();
        }

        public override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override object Remove(string key)
        {
            throw new NotImplementedException();
        }

        public override T Remove<T>(string key)
        {
            throw new NotImplementedException();
        }

        public override void Set(string key, object value, DateTimeOffset? absoluteExpiration = null)
        {
            throw new NotImplementedException();
        }

        public override void Set<T>(string key, T value, DateTimeOffset? absoluteExpiration = null)
        {
            throw new NotImplementedException();
        }

        public override void Set(string key, object value, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public override void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

    }
}
