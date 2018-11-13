using AnyCache.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnyCache.Serialization
{
    public class XmlSerializer : ISerializer
    {
        /// <summary>
        /// Including object type in the final serialized value.
        /// By setting this value to true the untyped Get() methods will be worked but it makes serialized value bigger.
        /// </summary>
        public bool SerializeTypeName { get; private set; }

        public XmlSerializer(bool serializeTypeName = false)
        {
            SerializeTypeName = serializeTypeName;
        }

        public void Serialize(object value, Stream stream)
        {
            using (StreamWriter sw = new StreamWriter(stream))
            {
                if (SerializeTypeName)
                    sw.WriteLine(value.GetType().AssemblyQualifiedName);

                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(value.GetType());
                serializer.Serialize(sw, value);
            }
        }

        public object Deserialize(Stream stream)
        {
            using (StreamReader sr = new StreamReader(stream))
            {
                if (SerializeTypeName)
                {
                    string className = sr.ReadLine();
                    Type objectType = Util.GetType(className);

                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(objectType);
                    return serializer.Deserialize(sr);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        public T Deserialize<T>(Stream stream)
        {
            using (StreamReader sr = new StreamReader(stream))
            {
                if (SerializeTypeName)
                {
                    string className = sr.ReadLine();
                    //Type objectType = GetType(className);
                }

                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(sr);
            }
        }
    }
}
