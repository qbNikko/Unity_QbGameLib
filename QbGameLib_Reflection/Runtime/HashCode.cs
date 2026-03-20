using System.Collections;
using System.Collections.Generic;

namespace QbGameLib.Reflection
{
    public class HashCodeUtils
    {
        public static int ArrayHashCode<T>(T[] value)
        {
            return value==null ? 0 : ((IStructuralEquatable)value).GetHashCode(EqualityComparer<T>.Default);
        }
    }
}