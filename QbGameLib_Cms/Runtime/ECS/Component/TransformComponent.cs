using System;
using UnityEngine;

namespace QbGameLib.Cms.ECS
{
    [Serializable]
    public struct TransformComponent : IComponent
    {
        public Transform transform;
        
    }
}