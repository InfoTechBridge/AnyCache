# AnyCache
.Net cache framework for connecting to any caches including in memory cache and Redis from dotnet, dotnet-core and Xamarin forms.

[![License](http://img.shields.io/:license-MIT-blue.svg)](https://raw.githubusercontent.com/giacomelli/JobSharp/master/LICENSE)

Features
===
- Common API for .Net Framework and .NetCore projects
- Common API for In Memory cache and Redis cache.

Usage
------

```csharp
cache.Set("key", 123000);
var ret = cache.Get<int?>("key");
```

Installation
-------------

AnyCache is available as a nuget package and supports InMemory and Redis cache at the moment.

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

