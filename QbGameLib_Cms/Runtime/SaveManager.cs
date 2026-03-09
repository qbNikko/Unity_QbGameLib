using System.Collections.Generic;
using UnityEngine;

namespace QbGameLib.Cms
{
    public class SaveManager<T>
    {
        private static SaveManager<T> _instance;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Reset()=> _instance = null;
        
        private static SaveManager<T> Instance
        {
            get
            {
                if (_instance == null) _instance = new SaveManager<T>();
                return _instance;
            }
        }
        
        public static void Add(ISaved<T> saveObject) => Instance.Add_(saveObject);

        public static void Remove(ISaved<T> saveObject) => Instance.Remove_(saveObject);
        
        public static void Clear() => Instance.Clear_();

        public static void Save(T data) => Instance.Save_(data);

        public static void Load(T data)=> Instance.Load_(data);


        
        
        
        private List<ISaved<T>> _saveObjects = new();
        
        public void Add_(ISaved<T> saveObject) => _saveObjects.Add(saveObject);
        public void Remove_(ISaved<T> saveObject) => _saveObjects.Remove(saveObject);
        public void Clear_()=> Instance.Clear_();

        public void Save_(T data)
        {
            _saveObjects.ForEach(s=>s.Save(data));
        }

        public void Load_(T data)
        {
            _saveObjects.ForEach(s=>s.Load(data));
        }
    }
}