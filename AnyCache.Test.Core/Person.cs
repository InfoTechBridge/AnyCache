using System;
using System.Collections.Generic;
using System.Text;

namespace AnyCache.Test.Core
{
    [Serializable]
    public class Person
    {
        public Person()
        {

        }

        public Person(string name, string family)
        {
            Name = name;
            Family = family;
        }
        public string Name { get; set; }
        public string Family { get; set; }
    }
}
