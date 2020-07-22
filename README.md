# AnyCache
.Net cache framework for connecting to any caches including in memory cache and Redis from dotnet, dotnet-core and Xamarin forms.

[![License](http://img.shields.io/:license-MIT-blue.svg)](https://raw.githubusercontent.com/giacomelli/JobSharp/master/LICENSE)

AnyCache is library for having common cache API for .Net framework and .Net Core as well as Xamarin Forms projects.
As you know, the MemoryCache API in .Net freamwork is not same as MemoryCache in .Net Core and we couldn't easly transfer our code between them. By AnyCache we could have one code base that could be reused in diffrent type of project without any code change. AnyCache also support Redis cachew whith same API as MemoryCache, so we could swap our project caching from MemoryCache to Redis cache and vice versa whith out any major code change. I have plan to add more type of chaching in the future.

Features
===
- Same API for .Net Framework, .Net Core projects and Xamarin Forms projects
- Same API for any type of caching including In Memory cache and Redis cache.

Basic usage
------

```csharp
cache.Set("key", 123000);
var ret = cache.Get<int?>("key");
```

```csharp
Person obj = new Person("Tom", "Hanks");
cache.Set(key, obj, null);

Person ret = cache.Get<Person>(key);
```

Installation
-------------

AnyCache is available as a nuget package and supports In Memory and Redis cache at the moment.

**In Memory Cache**

For creating in memory cache use following nuget command to add library to your project:

```
PM> Install-Package AnyCache.InMemory
```

Then use following code to create In Memory cache. 

```csharp
var cache = new InMemoryCache();
```

**Redis Cache**

For creating Redis cache use following nuget command to add library to your project:

```
PM> Install-Package AnyCache.Redis
PM> Install-Package AnyCache.Serialization
```

Then use following code to create Redis cache. 

```csharp
ISerializer serializer = new JsonSerializer(null, true);
var cache = new RedisCache(connectionString:"localhost", serializer:serializer);
```

List of available methods
-------------------------

```csharp
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

T GetValueOrDefault<T>(string key, T value);

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

IEnumerable<KeyValuePair<string, object>> GetAll(IEnumerable<string> keys);
IEnumerable<KeyValuePair<string, T>> GetAll<T>(IEnumerable<string> keys);

object Remove(string key);
T Remove<T>(string key);

long GetCount();

IEnumerator<KeyValuePair<string, object>> GetEnumerator();
        
void ClearCache();
void Compact();
```
