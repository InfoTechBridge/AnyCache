using System;
using System.Collections.Generic;
using System.Text;

namespace AnyCache.Serialization
{
    public static class Util
    {
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
