using System;
using System.Collections.Generic;
using UnityEngine;

namespace QbGameLib.Component
{
    public static class ReloadDomainManager
    {
#if UNITY_EDITOR 
        private static List<Action> _resetters = new List<Action>();
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Init()
        {
            _resetters.ForEach(action => action.Invoke());
            _resetters.Clear();
        }
#endif 
        public static void Subscribe(Action action)
        {
#if UNITY_EDITOR
            _resetters.Add(action);
#endif   
        }
        
        
        
        
    }
}