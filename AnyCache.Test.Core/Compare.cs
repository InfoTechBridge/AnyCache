using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyCache.Test.Core
{
    public static class Compare
    {


        public static bool PublicInstancePropertiesEqualLinq<T>(this T self, T to, params string[] ignore) where T : class
        {
            if (self != null && to != null)
            {
                var type = typeof(T);
                var ignoreList = new List<string>(ignore);
                var unequalProperties =
                    from pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                    where !ignoreList.Contains(pi.Name)
                    let selfValue = type.GetProperty(pi.Name).GetValue(self, null)
                    let toValue = type.GetProperty(pi.Name).GetValue(to, null)
                    where selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue))
                    select selfValue;
                return !unequalProperties.Any();
            }
            return self == to;
        }

        /// <summary>Compare the public instance properties. Uses deep comparison.</summary>
        /// <param name="self">The reference object.</param>
        /// <param name="to">The object to compare.</param>
        /// <param name="ignore">Ignore property with name.</param>
        /// <typeparam name="T">Type of objects.</typeparam>
        /// <returns><see cref="bool">True</see> if both objects are equal, else <see cref="bool">false</see>.</returns>
        public static bool PublicInstancePropertiesEqual<T>(this T self, T to, params string[] ignore) where T : class
        {
            if (self != null && to != null && !self.GetType().IsValueType)
            {
                var type = self.GetType();
                var ignoreList = new List<string>(ignore);
                foreach (var pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    if (ignoreList.Contains(pi.Name))
                        continue;

                    var selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                    var toValue = type.GetProperty(pi.Name).GetValue(to, null);

                    if (pi.PropertyType.IsClass && !pi.PropertyType.IsValueType && pi.PropertyType != typeof(string) && !pi.PropertyType.IsArray)
                    {
                        if (PublicInstancePropertiesEqual(selfValue, toValue, ignore))
                            continue;

                        return false;
                    }

                    if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                    {
                        return false;
                    }
                }

                return true;
            }

            return self == to;
        }
    }
}
