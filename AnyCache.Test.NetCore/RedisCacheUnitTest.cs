using AnyCache.InMemory;
using AnyCache.Redis;
using AnyCache.Serialization;
using AnyCache.Serialization.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyCache.Test.NetCore
{
    [TestClass]
    public class RedisCacheUnitTest : AnyCacheUnitTestBase
    {
        public RedisCacheUnitTest()
        {
            //ISerializer serializer = new BinarySerializer();
            ISerializer serializer = new JsonSerializer(null, true);
            //ISerializer serializer = new MsgPackSerializer();
            //ISerializer serializer = new XmlSerializer();
            //ISerializer serializer = new ProtoBufSerializer();

            //cache = new RedisCache(serializer: serializer);
            base.cache = new RedisCache(null, 1, "test_", serializer);
            //this line will be run before any test method run
            base.cache.ClearCache();
        }
    }
}
