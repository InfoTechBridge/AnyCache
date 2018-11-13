using AnyCache.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace AnyCache.Serialization
{
    public class INISerializer : ISerializer
    {
        private readonly StreamingContext Context;

        public INISerializer()
        {
            Context = new StreamingContext(StreamingContextStates.All);
        }

        public void Serialize(object value, Stream stream)
        {
            // Get fields that are to be serialized.
            MemberInfo[] members = FormatterServices.GetSerializableMembers(value.GetType(), Context);

            // Get fields data.
            object[] objs = FormatterServices.GetObjectData(value, members);

            // Write class name and all fields & values to stream
            using (StreamWriter sw = new StreamWriter(stream))
            {
                sw.WriteLine(value.GetType().AssemblyQualifiedName);
                for (int i = 0; i < objs.Length; ++i)
                {
                    sw.WriteLine("{0}={1}", members[i].Name, objs[i].ToString());
                }
                sw.Close();
            }
        }

        public object Deserialize(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);

            string className = sr.ReadLine();
            Type t = Type.GetType(className);

            // Create object of just found type name.
            Object obj = FormatterServices.GetUninitializedObject(t);

            // Get type members.
            MemberInfo[] members = FormatterServices.GetSerializableMembers(obj.GetType(), Context);

            // Create data array for each member.
            object[] data = new object[members.Length];

            // Store serialized variable name -> value pairs.
            StringDictionary values = new StringDictionary();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(new char[] { '=' });

                // key = variable name, value = variable value.
                values[tokens[0]] = line.Replace(tokens[0] + "=", string.Empty);
            }
            sr.Close();

            // Store for each member its value, converted from string to its type.
            for (int i = 0; i < members.Length; ++i)
            {
                FieldInfo fi = ((FieldInfo)members[i]);
                if (!values.ContainsKey(fi.Name))
                    throw new SerializationException("Missing field value : " + fi.Name);
                data[i] = System.Convert.ChangeType(values[fi.Name], fi.FieldType);
            }

            // Populate object members with theri values and return object.
            return FormatterServices.PopulateObjectMembers(obj, members, data);
        }

        public T Deserialize<T>(Stream stream)
        {
            throw new NotImplementedException();
        }

       
    }
}
