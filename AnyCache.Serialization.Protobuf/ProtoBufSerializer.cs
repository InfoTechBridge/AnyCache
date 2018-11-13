using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnyCache.Serialization.Protobuf
{
    public class ProtoBufSerializer : ISerializer
    {
        /// <summary>
        /// Including object type in the final serialized value.
        /// By setting this value to true the untyped Get() methods will be worked but it makes serialized value bigger.
        /// </summary>
        public bool SerializeTypeName { get; private set; }

        public ProtoBufSerializer(bool serializeTypeName = false)
        {
            SerializeTypeName = serializeTypeName;
        }

        public void Serialize(object value, Stream stream)
        {
            using (BinaryWriter bw = new BinaryWriter(stream))
            {
                if (SerializeTypeName)
                    bw.Write(value.GetType().AssemblyQualifiedName);
                
                Serializer.Serialize(stream, value);
            }
        }

        public object Deserialize(Stream stream)
        {
            using (BinaryReader br = new BinaryReader(stream))
            {
                if (SerializeTypeName)
                {
                    string className = br.ReadString();
                    Type objectType = GetType(className);

                    return Serializer.Deserialize(objectType, stream);
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

                return Serializer.Deserialize<T>(stream);
            }
        }

        internal static Type GetType(string typeName)
        {
            // If typeName is just FullName of the class It dos not returns type of classes that exist in another dll library
            // but if typeName is AssemblyQualifiedName ("typeName,DllName" format) it will be ok.
            var type = Type.GetType(typeName);
            if (type != null) return type;
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(typeName);
                if (type != null)
                    break;
            }
            return type;
        }
    }
}
