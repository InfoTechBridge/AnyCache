using AnyCache.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace AnyCache.Test.Core
{
    //[TestClass]
    public abstract class AnyCacheUnitTestBase
    {

        protected IAnyCache cache;

        [TestMethod]
        public void Get_Value()
        {
            string key = "key1";
            cache.Set(key, 123, null);

            var ret = (int?)cache.Get(key);

            Assert.AreEqual(123, ret);
        }

        [TestMethod]
        public void Get_Value_Of_T()
        {
            string key = "key1typed";
            cache.Set(key, 123000, null);

            var ret = cache.Get<int?>(key);

            Assert.AreEqual(123000, ret);
        }

        [TestMethod]
        public void Get_Null_Value()
        {
            string key = "key1";
            cache.Set(key, null, null);

            var ret = (int?)cache.Get(key);

            Assert.IsNull(ret);
        }

        [TestMethod]
        public void Get_Null_Value_Of_T()
        {
            string key = "key1typed";
            cache.Set(key, null, null);

            var ret = cache.Get<int?>(key);

            Assert.IsNull(ret);
        }

        [TestMethod]
        public void Get_Null_Object()
        {
            string key = "key1";
            cache.Set(key, null, null);

            var ret = (Person)cache.Get(key);

            Assert.IsNull(ret);
        }

        [TestMethod]
        public void Get_Null_Object_Of_T()
        {
            string key = "key1typed";
            cache.Set(key, null, null);

            var ret = cache.Get<Person>(key);

            Assert.IsNull(ret);
        }

        [TestMethod]
        public void Get_Object_Whene_NotExist()
        {
            string key = "TestNotExistValue";

            var ret = cache.Get(key);

            Assert.IsNull(ret);

        }

        [TestMethod]
        public void Get_Value_Of_T_Whene_NotExist()
        {
            string key = "TestNotExistValue";

            var ret = cache.Get<int?>(key);

            Assert.IsNull(ret);

        }

        [TestMethod]
        public void Get_Object_Of_T_Whene_NotExist()
        {
            string key = "TestNotExistValue";

            var ret = cache.Get<Person>(key);

            Assert.IsNull(ret);

        }

        [TestMethod]
        public void Get_Object()
        {
            string key = "key1obj";
            Person obj = new Person("Tom", "Hanks");
            cache.Set(key, obj, null);

            Person ret = (Person)cache.Get(key);

            Assert.IsTrue(obj.PublicInstancePropertiesEqual(ret));

        }

        [TestMethod]
        public void Get_Object_Of_T()
        {
            string key = "key1obj";
            Person obj = new Person("Tom", "Hanks");
            cache.Set(key, obj, null);

            Person ret = cache.Get<Person>(key);

            Assert.IsTrue(obj.PublicInstancePropertiesEqual(ret));

        }

        [TestMethod]
        public void Get_List_Of_T_Objects()
        {
            string key = "key2list";
            List<Person> list = new List<Person>();
            list.Add(new Person("Tom", "Hanks"));
            list.Add(new Person("Tom1", "Hanks1"));
            list.Add(new Person("Tom2", "Hanks2"));
            list.Add(new Person("Tom3", "Hanks3"));

            cache.Set(key, list, null);

            //List<Person> ret = (List<Person>)cache.Get(key);
            List<Person> ret = cache.Get<List<Person>>(key);

            Assert.AreEqual(list.Count, ret.Count);
            Assert.IsTrue(list[0].PublicInstancePropertiesEqual(ret[0]));
            Assert.IsTrue(list[1].PublicInstancePropertiesEqual(ret[1]));
            Assert.IsTrue(list[2].PublicInstancePropertiesEqual(ret[2]));
            Assert.IsTrue(list[3].PublicInstancePropertiesEqual(ret[3]));
        }

        [TestMethod]
        public void Get_List_Of_Objects()
        {
            string key = "key2list";
            List<Person> list = new List<Person>();
            list.Add(new Person("Tom", "Hanks"));
            list.Add(new Person("Tom1", "Hanks1"));
            list.Add(new Person("Tom2", "Hanks2"));
            list.Add(new Person("Tom3", "Hanks3"));

            cache.Set(key, list, null);

            List<Person> ret = (List<Person>)cache.Get(key);

            Assert.AreEqual(list.Count, ret.Count);
            Assert.IsTrue(list[0].PublicInstancePropertiesEqual(ret[0]));
            Assert.IsTrue(list[1].PublicInstancePropertiesEqual(ret[1]));
            Assert.IsTrue(list[2].PublicInstancePropertiesEqual(ret[2]));
            Assert.IsTrue(list[3].PublicInstancePropertiesEqual(ret[3]));
        }

        [TestMethod]
        public void GetEnumerator()
        {
            int count = 10;
            for(int i = 0; i < count; i++)
                cache.Set($"key{i}", $"value {i}", null);
                        
            var list = cache.AsEnumerable<KeyValuePair<string, object>>().ToList();

            Assert.AreEqual(list.Count, count);
            list.Sort((x,y) => x.Key.CompareTo(y.Key));
            for (int i = 0; i < count; i++)
                Assert.IsTrue(list[i].Key == $"key{i}" && list[i].Value.ToString() == $"value {i}");
        }

        [TestMethod]
        public void GetAll_Objects_By_Keys()
        {
            int count = 10;
            List<string> keys = new List<string>();
            for (int i = 0; i < count; i++)
            {
                keys.Add($"key{i}");
                cache.Set($"key{i}", $"value {i}", null);
            }

            var list = cache.GetAll(keys).ToList();

            Assert.AreEqual(list.Count, count);
            list.Sort((x, y) => x.Key.CompareTo(y.Key));
            for (int i = 0; i < count; i++)
                Assert.IsTrue(list[i].Key == $"key{i}" && list[i].Value.ToString() == $"value {i}");
        }

        [TestMethod]
        public void GetAll_Objects_Of_T_By_Keys()
        {
            int count = 10;
            List<string> keys = new List<string>();
            for (int i = 0; i < count; i++)
            {
                keys.Add($"key{i}");
                cache.Set($"key{i}", $"value {i}", null);
            }

            var list = cache.GetAll<string>(keys).ToList();

            Assert.AreEqual(list.Count, count);
            list.Sort((x, y) => x.Key.CompareTo(y.Key));
            for (int i = 0; i < count; i++)
                Assert.IsTrue(list[i].Key == $"key{i}" && list[i].Value == $"value {i}");
        }
    }
}
