using System;
using System.Collections;

namespace Chronicy.Utils
{
    public static class TypeUtils
    {
        public static bool IsEnumerable(this Type type)
        {
            return (type.GetInterface(nameof(IEnumerable)) != null);
        }

        public static bool IsCollection(this Type type)
        {
            return (type.GetInterface(nameof(ICollection)) != null);
        }
    }
}
