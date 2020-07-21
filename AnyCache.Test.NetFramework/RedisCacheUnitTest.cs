using AnyCache.Redis;
using AnyCache.Serialization;
using AnyCache.Serialization.Protobuf;
using AnyCache.Test.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyCache.Test.NetFramework
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
            base.cache = new RedisCache(null, 2, "test_dotnet_framework", serializer);
            //this line will be run before any test method run
            base.cache.ClearCache();
        }
    }
}
