using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace QbGameLib.Cms
{
    public class ResourceManager
    {
        private static ResourceManager _instance;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Reset()=> _instance = null;
        
        private static ResourceManager Instance
        {
            get
            {
                if (_instance == null) _instance = new ResourceManager();
                return _instance;
            }
        }

        public static List<T> FindAssets<T>() where T : Object
        {
            List<T> list = new List<T>();
            _instance.FindAssets_<T>(ref list);
            return list;
        }
        
        public static void FindAssets<T>(ref List<T> list) where T : Object
        {
            _instance.FindAssets_<T>(ref list);
        }

        public static T Instantiate<T>(T prefub)  where T : Object
        {
            return _instance.Instantiate_(prefub);
        }
        
        public T Instantiate_<T>(T prefub)  where T : Object
        {
            return Object.Instantiate(prefub);
        }
        
        public void FindAssets_<T>(ref List<T> list) where T : Object
        {
#if UNITY_EDITOR
            string[] findAssets = AssetDatabase.FindAssets($"t:{nameof(T)}");
            list.Clear();
            foreach (string name in findAssets)
            {
                var asset = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(name));
                list.Add(asset);
            };
#else
            throw new System.NotSupportedException($"Method support only Editor");
#endif
        }
    }
}