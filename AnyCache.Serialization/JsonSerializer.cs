using AnyCache.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;

namespace AnyCache.Serialization
{
    public class JsonSerializer : ISerializer
    {
        private readonly Encoding encoding;

        /// <summary>
        /// Including object type in the final serialized value.
        /// By setting this value to true the untyped Get() methods will be worked but it makes serialized value bigger.
        /// </summary>
        public bool SerializeTypeName { get; private set; }
     
        public JsonSerializer(Encoding encoding = null, bool serializeTypeName = false)
        {
            if (encoding != null)
                this.encoding = encoding;
            else
                this.encoding = Encoding.Default;

            SerializeTypeName = serializeTypeName;
        }

        //public string Serialize(object value)
        //{
        //    return JsonConvert.SerializeObject(value);
        //}

        //public object Deserialize(string value)
        //{
        //    return JsonConvert.DeserializeObject(value);
        //}

        //public T Deserialize<T>(string value)
        //{
        //    return JsonConvert.DeserializeObject<T>(value);
        //}

        public void Serialize(object value, Stream stream)
        {
            using (StreamWriter sw = new StreamWriter(stream, encoding))
            {
                if (SerializeTypeName)
                    sw.WriteLine(value.GetType().AssemblyQualifiedName);

                sw.Write(JsonConvert.SerializeObject(value));
            }
        }

        public object Deserialize(Stream stream)
        {
            using (StreamReader sr = new StreamReader(stream, encoding))
            {
                if (SerializeTypeName)
                {
                    string className = sr.ReadLine();
                    Type objectType = Util.GetType(className);

                    return JsonConvert.DeserializeObject(sr.ReadToEnd(), objectType);
                }
                else
                    return JsonConvert.DeserializeObject(sr.ReadToEnd());
            }
        }

        public T Deserialize<T>(Stream stream)
        {
            using (StreamReader sr = new StreamReader(stream, encoding))
            {
                if (SerializeTypeName)
                {
                    string className = sr.ReadLine();
                    //Type objectType = GetType(className);
                }

                return JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
            }
        }
       
    }
}
