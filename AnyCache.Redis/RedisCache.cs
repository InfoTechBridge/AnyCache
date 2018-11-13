using AnyCache.Core;
using AnyCache.Serialization;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AnyCache.Redis
{
    public class RedisCache : CacheBase, IAnyHashCache, IDisposable
    {
        public readonly string KeyPrefix;
        private readonly ISerializer _serializer;

        /// <summary>
        /// The ConnectionMultiplexer should attempt to reconnect automatically. So, should not need to dispose/reconnect.
        /// You can monitor the connection failure/reconnect via events published on the multiplexer instance.
        /// You can also use the .IsConnected() method on a database (this takes a key for server targeting reasons, but if you are only talking to one server, you could pass anything as the key).
        /// Marc Gravell
        /// </summary>
        ConnectionMultiplexer _redis;
        IDatabase _db;


        public RedisCache(string connectionString = null, string keyPrefix = null, ISerializer serializer = null)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                _redis = ConnectionMultiplexer.Connect("localhost");
            else
                _redis = ConnectionMultiplexer.Connect(connectionString);
            _redis.PreserveAsyncOrder = false;

            _db = _redis.GetDatabase();

            KeyPrefix = keyPrefix;

            if (serializer == null)
                _serializer = new JsonSerializer();
            else
                _serializer = serializer;
        }

        protected string ToRedisKey(string key)
        {
            if (string.IsNullOrWhiteSpace(KeyPrefix))
                return key;

            return $"{KeyPrefix}:{key}";
        }

        protected string FromRedisKey(string key)
        {
            if (string.IsNullOrWhiteSpace(KeyPrefix))
                return key;

            return key.Remove(0, KeyPrefix.Length + 1);
        }

        protected RedisValue ToRedisValue(object obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                _serializer.Serialize(obj, stream);
                byte[] buffer = stream.ToArray();
                return buffer;
            }
        }

        protected object FromRedisValue(RedisValue value)
        {
            using (MemoryStream stream = new MemoryStream(value))
            {
                object retObject = _serializer.Deserialize(stream);
                return retObject;
            }
        }

        protected T FromRedisValue<T>(RedisValue value)
        {
            using (MemoryStream stream = new MemoryStream(value))
            {
                T retObject = _serializer.Deserialize<T>(stream);
                return retObject;
            }
        }

        //public object this[string key] { get => Get(key); set => Set(key, value); }

        public override bool Add(string key, object value, DateTimeOffset? absoluteExpiration = null)
        {
            return _db.StringSet(ToRedisKey(key), ToRedisValue(value), absoluteExpiration.HasValue ? (TimeSpan?)(absoluteExpiration.Value - DateTimeOffset.Now) : null, When.NotExists);
        }

        public override bool Add<T>(string key, T value, DateTimeOffset? absoluteExpiration = null)
        {
            return _db.StringSet(ToRedisKey(key), ToRedisValue(value), absoluteExpiration.HasValue ? (TimeSpan?)(absoluteExpiration.Value - DateTimeOffset.Now) : null, When.NotExists);
        }

        public override bool Add(string key, object value, TimeSpan slidingExpiration)
        {
            return _db.StringSet(ToRedisKey(key), ToRedisValue(value), slidingExpiration, When.NotExists);
        }

        public override bool Add<T>(string key, T value, TimeSpan slidingExpiration)
        {
            return _db.StringSet(ToRedisKey(key), ToRedisValue(value), slidingExpiration, When.NotExists);
        }

        //public object AddOrGetExisting(string key, object value, DateTimeOffset? absoluteExpiration = null)
        //{
        //    var val = Get(key);
        //    if (val != null)
        //        return val;
        //    else
        //    {
        //        Set(key, value, absoluteExpiration);
        //        return value;
        //    }
        //}

        //public T AddOrGetExisting<T>(string key, T value, DateTimeOffset? absoluteExpiration = null)
        //{
        //    var val = Get<T>(key);
        //    if (val != null)
        //        return val;
        //    else
        //    {
        //        Set<T>(key, value, absoluteExpiration);
        //        return value;
        //    }
        //}

        //public object AddOrGetExisting(string key, object value, TimeSpan slidingExpiration)
        //{
        //    var val = Get(key);
        //    if (val != null)
        //        return val;
        //    else
        //    {
        //        Set(key, value, slidingExpiration);
        //        return value;
        //    }
        //}

        //public T AddOrGetExisting<T>(string key, T value, TimeSpan slidingExpiration)
        //{
        //    var val = Get<T>(key);
        //    if (val != null)
        //        return val;
        //    else
        //    {
        //        Set<T>(key, value, slidingExpiration);
        //        return value;
        //    }
        //}

        //public T AddOrGetExisting<T>(string key, Func<T> retriever, TimeSpan slidingExpiration)
        //{
        //    var val = Get<T>(key);
        //    if (val != null)
        //        return val;
        //    else
        //    {
        //        var value = retriever.Invoke();
        //        if (value != null)
        //            Set<T>(key, value, slidingExpiration);

        //        return value;
        //    }
        //}

        public override void Set(string key, object value, DateTimeOffset? absoluteExpiration = null)
        {
            _db.StringSet(ToRedisKey(key), ToRedisValue(value), absoluteExpiration.HasValue ? (TimeSpan?)(absoluteExpiration.Value - DateTimeOffset.Now) : null);
        }

        public override void Set<T>(string key, T value, DateTimeOffset? absoluteExpiration = null)
        {
            _db.StringSet(ToRedisKey(key), ToRedisValue(value), absoluteExpiration.HasValue ? (TimeSpan?)(absoluteExpiration.Value - DateTimeOffset.Now) : null);
        }

        public override void Set(string key, object value, TimeSpan slidingExpiration)
        {
            _db.StringSet(ToRedisKey(key), ToRedisValue(value), slidingExpiration);
        }

        public override void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            _db.StringSet(ToRedisKey(key), ToRedisValue(value), slidingExpiration);
        }

        public override bool Contains(string key)
        {
            return _db.KeyExists(ToRedisKey(key));
        }

        public override object Get(string key)
        {
            var val = _db.StringGet(ToRedisKey(key));
            if (!val.IsNull)
                return FromRedisValue(val);
            else
                return null;
        }

        public override T Get<T>(string key)
        {
            var val = _db.StringGet(ToRedisKey(key));
            if (!val.IsNull)
                return FromRedisValue<T>(val);
            else
            {
                //if (default(T) == null)
                return default(T);
                //else
                //    throw new EntryNotFoundException();
            }
        }

        //public T GetValueOrDefault<T>(string key)
        //{
        //    var val = _db.StringGet(ToRedisKey(key));
        //    if (!val.IsNull)
        //        return FromRedisValue<T>(val);
        //    else
        //        return default(T);

        //    //int? t;
        //    //t.GetValueOrDefault();
        //    //t.tr
        //}

        public override T GetValueOrDefault<T>(string key, T value)
        {
            var val = _db.StringGet(ToRedisKey(key));
            if (!val.IsNull)
                return FromRedisValue<T>(val);
            else
                return value;
        }

        //public bool TryGetValue(string key, out object result)
        //{

        //    var val = _db.StringGet(ToRedisKey(key));
        //    if (!val.IsNull)
        //    {
        //        result = FromRedisValue(val);
        //        return true;
        //    }
        //    else
        //    {
        //        result = null;
        //        return false;
        //    }
        //}

        //public bool TryGetValue<T>(string key, out T result)
        //{
        //    var val = _db.StringGet(ToRedisKey(key));
        //    if (!val.IsNull)
        //    {
        //        result = FromRedisValue<T>(val);
        //        return true;
        //    }
        //    else
        //    {
        //        result = default(T);
        //        return false;
        //    }
        //}

        public override async Task<object> GetAsync(string key)
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

        public override async Task<T> GetAsync<T>(string key)
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

        public override IDictionary<string, object> GetAll(IEnumerable<string> keys)
        {
            IDictionary<string, object> values = new Dictionary<string, object>();
            if (keys == null || keys.Count() == 0)
                return values;
            
            //var items = _db.StringGet(keys.Select(k => (RedisKey)ToRedisKey(k)).ToArray());
            //int i = 0;
            //foreach (var val in items)
            //{
            //    if (!val.IsNull)
            //        values.Add(keys[i++], FromRedisValue(val));
            //}
            foreach (var key in keys)
            {
                var val = _db.StringGet(ToRedisKey(key));
                if (!val.IsNull)
                    values.Add(key, FromRedisValue(val));
            }
            return values;
        }

        public override IDictionary<string, T> GetAll<T>(IEnumerable<string> keys)
        {
            IDictionary<string, T> values = new Dictionary<string, T>();
            foreach (var key in keys)
            {
                var val = _db.StringGet(ToRedisKey(key));
                if (!val.IsNull)
                    values.Add(key, FromRedisValue<T>((string)val));
            }
            return values;
        }

        public override object Remove(string key)
        {
            var val = Get(key);
            if (val != null)
                _db.KeyDelete(ToRedisKey(key));

            return null;
        }

        public override T Remove<T>(string key)
        {
            var val = Get<T>(key);
            if (val != null)
                _db.KeyDelete(ToRedisKey(key));

            return val;
        }


        public override long GetCount()
        {
            var counts = 0;
            var endpoints = _redis.GetEndPoints();
            foreach (var endpoint in endpoints)
            {
                var server = _redis.GetServer(endpoint);
                counts += server.Keys(pattern: ToRedisKey("*")).Count();
            }
            return counts;
        }

        public override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            var endpoints = _redis.GetEndPoints();
            foreach (var endpoint in endpoints)
            {
                var server = _redis.GetServer(endpoint);
                var keys = server.Keys(pattern: ToRedisKey("*"));
                foreach (var key in keys)
                {
                    var val = _db.StringGet(key);
                    if (!val.IsNull)
                        yield return new KeyValuePair<string, object>(FromRedisKey(key), FromRedisValue(val));
                }
            }
        }

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}

        public override void ClearCache()
        {
            var endpoints = _redis.GetEndPoints();
            foreach (var endpoint in endpoints)
            {
                var server = _redis.GetServer(endpoint);
                //server.FlushDatabase(_db.Database);

                var keys = server.Keys(pattern: ToRedisKey("*")).ToArray();
                if (keys.Count() > 0)
                    _db.KeyDelete(keys);
            }
        }

        public override void Compact()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _redis.Dispose();
        }

        public Task AddOrUpdateHashAsync(string key, List<KeyValuePair<string, object>> values)
        {
            List<HashEntry> items = new List<HashEntry>();
            foreach (var item in values)
                items.Add(new HashEntry((RedisValue)item.Key, ToRedisValue(item.Value)));

            return _db.HashSetAsync(ToRedisKey(key), items.ToArray());
        }
        public Task AddOrUpdateHashAsync(string key, string name, object value)
        {
            //await Task.Run( () => {
            //    var v = _serializer.SerializeObject(value);
            //    _db.HashSet(ToRedisKey(key), name, (RedisValue)value);
            //});


            return _db.HashSetAsync(ToRedisKey(key), name, ToRedisValue(value));
        }
        public async Task<List<KeyValuePair<string, object>>> GetHashAsync(string key)
        {
            List<KeyValuePair<string, object>> values = new List<KeyValuePair<string, object>>();
            var allHash = await _db.HashGetAllAsync(ToRedisKey(key));
            if (allHash != null)
            {
                foreach (var item in allHash)
                    values.Add(new KeyValuePair<string, object>(item.Name, FromRedisValue(item.Value)));

                return values;
            }
            else
                return null;
        }
        public async Task<object> GetHashAsync(string key, string name)
        {
            var val = await _db.HashGetAsync(ToRedisKey(key), name);
            if (!val.IsNull)
                return FromRedisValue(val);
            else
                return null;
        }

        public async Task<T> GetHashAsync<T>(string key, string name)
        {
            var val = await _db.HashGetAsync(ToRedisKey(key), name);
            if (!val.IsNull)
                //return (T)((object)val);
                return FromRedisValue<T>(val);
            else
                return default(T);
        }

        public async Task<long> IncrementHashAsync(string key, string name, long value)
        {
            var val = await _db.HashIncrementAsync(ToRedisKey(key), name, value, CommandFlags.HighPriority);
            return val;
        }
    }
}
