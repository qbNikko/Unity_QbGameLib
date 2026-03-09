using System;
using UnityEngine.Pool;

namespace QbGameLib.Pool
{
    public class ObjectPoolAny<TItem> where  TItem : class, new()
    {
        internal static readonly UnityEngine.Pool.ObjectPool<TItem> s_Pool = new UnityEngine.Pool.ObjectPool<TItem>((Func<TItem>) (() => new TItem()));
        
        public static TItem Get() => s_Pool.Get();

        public static PooledObject<TItem> Get(out TItem value)
        {
            return s_Pool.Get(out value);
        }

        public static void Release(TItem toRelease)
        {
            s_Pool.Release(toRelease);
        }
    }
    
    public class ObjectPoolPooled<TItem> where  TItem : class, IPooled, new()
    {
        internal static readonly UnityEngine.Pool.ObjectPool<TItem> s_Pool = new UnityEngine.Pool.ObjectPool<TItem>((Func<TItem>) (() => new TItem()), actionOnRelease: (Action<TItem>) (l => l.Clear()));
        
        public static TItem Get() => s_Pool.Get();

        public static PooledObject<TItem> Get(out TItem value)
        {
            return s_Pool.Get(out value);
        }

        public static void Release(TItem toRelease)
        {
            s_Pool.Release(toRelease);
        }
    }
}