using AnyCache.InMemory;
using AnyCache.Test.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyCache.Test.NetFramework
{
    [TestClass]
    public class InMemoryCacheUnitTest : AnyCacheUnitTestBase
    {
        public InMemoryCacheUnitTest()
        {
            base.cache = new InMemoryCache();

            //this line will be run before any test method run
            //base.cache.ClearCache();
        }
    }
}
