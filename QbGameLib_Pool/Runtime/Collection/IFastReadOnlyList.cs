using System.Collections.Generic;

namespace QbGameLib.Pool.Collection
{
    public interface IFastReadOnlyList<T> : IReadOnlyList<T>
    {
        public ref T Get(int idx);
    }
}