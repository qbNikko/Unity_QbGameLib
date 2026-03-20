using System.Collections.Generic;
using QbGameLib.Pool.Collection;
using UnityEngine.Pool;

namespace QbGameLib.Pool
{
    public static class Pools
    {
        //Collection
        public static PooledObject<FastList<T>> GetFastList<T>(out FastList<T> obj)
        {
            return CollectionPool<FastList<T>,T>.Get(out obj);
        }
        
        public static PooledObject<List<T>> GetList<T>(out List<T> obj)
        {
            return CollectionPool<List<T>,T>.Get(out obj);
        }
        
        public static PooledObject<HashSet<T>> GetSet<T>(out HashSet<T> obj)
        {
            return CollectionPool<HashSet<T>,T>.Get(out obj);
        }
        
        public static PooledObject<Dictionary<K,V>> GetDictionary<K,V>(out Dictionary<K,V>  obj)
        {
            return DictionaryPool<K,V>.Get(out obj);
        }
        
        public static void ReleaseFastList<T>(FastList<T> obj)
        {
            CollectionPool<FastList<T>,T>.Release(obj);
        }
        
        public static void ReleaseList<T>(List<T> obj)
        {
            CollectionPool<List<T>,T>.Release(obj);
        }
        
        public static void ReleaseSet<T>(HashSet<T> obj)
        {
            CollectionPool<HashSet<T>,T>.Release(obj);
        }
        
        public static void ReleaseDictionary<K,V>(Dictionary<K,V>  obj)
        {
            DictionaryPool<K,V>.Release(obj);
        }
        
        //Collection
    }
}