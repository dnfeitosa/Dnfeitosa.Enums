using System;
using System.Linq;
using System.Reflection;

namespace Dnfeitosa.Enums.Helpers
{
    internal static class TypeExtensions
    {
        internal static bool IsAnEnumType(this Type type)
        {
            return type
                .GetInterfaces()
                .Any(iface => iface == typeof(IEnum));;
        }

        internal static FieldInfo GetFieldNamed(this Type type, string name)
        {
            if (type == null)
            {
                return null;
            }

            return type
                .GetFields(BindingFlags.Static | BindingFlags.Public)
                .Where(f => f.Name == name)
                .FirstOrDefault();
        }
    }
}
