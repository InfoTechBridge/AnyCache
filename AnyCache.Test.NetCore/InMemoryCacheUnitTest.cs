using AnyCache.InMemory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyCache.Test.NetCore
{
    [TestClass]
    public class InMemoryCacheUnitTest : AnyCacheUnitTestBase
    {
        public InMemoryCacheUnitTest()
        {
            base.cache = new InMemoryCache();
        }
    }
}
