using AnyCache.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnyCache.Serialization
{
    public class StringSerializer : ISerializer
    {
        public object Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }

        public T Deserialize<T>(Stream stream)
        {
            throw new NotImplementedException();
        }

        public void Serialize(object value, Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
