using System;
using System.Reflection;

namespace A4CoreBlog.Data.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static T CastTo<T>(this object obj)
        {
            var result = Activator.CreateInstance(typeof(T));

            foreach (var property in obj.GetType().GetTypeInfo().DeclaredProperties)
            {
                try
                {
                    result.GetType().GetTypeInfo().GetDeclaredProperty(property.Name).SetValue(result, property.GetValue(obj));
                }
                catch
                {
                    continue;
                }
            }

            return (T)result;
        }
    }
}
