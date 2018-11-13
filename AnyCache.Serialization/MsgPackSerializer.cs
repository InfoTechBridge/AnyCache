using AnyCache.Core;
using MsgPack;
using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnyCache.Serialization
{
    public class MsgPackSerializer : ISerializer
    {
        /// <summary>
        /// Including object type in the final serialized value.
        /// By setting this value to true the untyped Get() methods will be worked but it makes serialized value bigger.
        /// </summary>
        public bool SerializeTypeName { get; private set; }

        public MsgPackSerializer(bool serializeTypeName = false)
        {
            SerializeTypeName = serializeTypeName;
        }

        public void Serialize(object value, Stream stream)
        {
            using (BinaryWriter bw = new BinaryWriter(stream))
            {
                if (SerializeTypeName)
                    bw.Write(value.GetType().AssemblyQualifiedName);
                
                var serializer = MessagePackSerializer.Get(value.GetType());
                serializer.Pack(stream, value);
            }
        }

        public object Deserialize(Stream stream)
        {
            using (BinaryReader br = new BinaryReader(stream))
            {
                if (SerializeTypeName)
                {
                    string className = br.ReadString();
                    Type objectType = Util.GetType(className);

                    var serializer = MessagePackSerializer.Get(objectType);
                    return serializer.Unpack(stream);
                }
                else
                {
                    throw new NotImplementedException();
                    //// Gets an object which stores dynamic key/value pairs as dictionary object.
                    //var serializer = MessagePackSerializer.Get<Dictionary<MessagePackObject, MessagePackObject>>();
                    //retObject = serializer.Unpack(stream);
                }
            }
        }

        public T Deserialize<T>(Stream stream)
        {
            using (BinaryReader br = new BinaryReader(stream))
            {
                if (SerializeTypeName)
                {
                    string className = br.ReadString();
                    //Type objectType = Util.GetType(className);
                }

                var serializer = MessagePackSerializer.Get(typeof(T));
                return (T)serializer.Unpack(stream);
            }
        }
    }
}
