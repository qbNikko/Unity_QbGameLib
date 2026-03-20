using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using QbGameLib.Pool;
using QbGameLib.Pool.Collection;
using QbGameLib.Reflection;
using UnityEngine;

namespace QbGameLib.Cms.ECS
{
    public delegate void EntityQueryActionRef(ref Entity entity);
    public delegate void EntityIndexActionRef(int entity, World world, EntityQueryActionRef actionRef);
    public partial class EntityQuery
    {
        private World _world;
        private int[] _need;
        private int[] _skip;
        private Action<EntityIndexActionRef, EntityQueryActionRef> _getEntityFunc;
        private EntityIndexActionRef _iterateAction;
        
        internal EntityQuery(World world)
        {
            _world = world;
            _iterateAction = (entitIdx, world, action) =>
            {
                ref Entity entity = ref world.GetEntity(entitIdx);
                if (!CheckNeed(ref entity)) return;
                if (!CheckSkip(ref entity)) return;
                action.Invoke(ref entity);
            };
        }

        public void Iterate(EntityQueryActionRef actionRef)
        {
            _getEntityFunc.Invoke(_iterateAction, actionRef);
        }

        private bool CheckNeed(ref  Entity entity)
        {
            if (_need == null) return true;
            for (var i1 = 0; i1 < _need.Length; i1++)
            {
                if (entity._components[_need[i1]] == -1)return false;
            }
            return true;
        }
        
        private bool CheckSkip(ref Entity entity)
        {
            if (_skip == null) return true;
            for (var i1 = 0; i1 < _skip.Length; i1++)
            {
                if (entity._components[_skip[i1]] != -1) return false;
            }
            return true;
        }
    }
    

    public partial class EntityQuery
    {
        public EntityQuery Component<T1>() where T1 : struct, IComponent
        {
            _getEntityFunc = (action, iterateAction)=>_world.GetComponentPool<T1>().IterateEntity(action, iterateAction);
            return this;
        }
        
        public EntityQuery Component<T1,T2>() 
            where T1 : struct, IComponent
            where T2 : struct, IComponent
        {
            _getEntityFunc = (action, iterateAction)=>_world.GetComponentPool<T1>().IterateEntity(action, iterateAction);
            _need = new[] { ComponentsContainer.Get.ComponentIndex(typeof(T2)) };
            return this;
        }
        
        public EntityQuery Component<T1,T2,T3>() 
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
        {
            _getEntityFunc = (action, iterateAction)=>_world.GetComponentPool<T1>().IterateEntity(action, iterateAction);
            _need = new[]
            {
                ComponentsContainer.Get.ComponentIndex(typeof(T2)),
                ComponentsContainer.Get.ComponentIndex(typeof(T3))
            };
            return this;
        }
        
        public EntityQuery Component<T1,T2,T3,T4>() 
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
        {
            _getEntityFunc = (action, iterateAction)=>_world.GetComponentPool<T1>().IterateEntity(action, iterateAction);
            _need = new[] { ComponentsContainer.Get.ComponentIndex(typeof(T2)),
                ComponentsContainer.Get.ComponentIndex(typeof(T3)),
                ComponentsContainer.Get.ComponentIndex(typeof(T4)) };
            return this;
        }
        
        public EntityQuery Component<T1,T2,T3,T4,T5>() 
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
        {
            _getEntityFunc = (action, iterateAction)=>_world.GetComponentPool<T1>().IterateEntity(action, iterateAction);
            _need = new[] { ComponentsContainer.Get.ComponentIndex(typeof(T2)),
                ComponentsContainer.Get.ComponentIndex(typeof(T3)),
                ComponentsContainer.Get.ComponentIndex(typeof(T4)),
                ComponentsContainer.Get.ComponentIndex(typeof(T5)) };
            return this;
        }
        
        public EntityQuery Component<T1,T2,T3,T4,T5,T6>() 
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
        {
            _getEntityFunc = (action, iterateAction)=>_world.GetComponentPool<T1>().IterateEntity(action, iterateAction);
            _need = new[] { ComponentsContainer.Get.ComponentIndex(typeof(T2)),
                ComponentsContainer.Get.ComponentIndex(typeof(T3)),
                ComponentsContainer.Get.ComponentIndex(typeof(T4)),
                ComponentsContainer.Get.ComponentIndex(typeof(T5)),
                ComponentsContainer.Get.ComponentIndex(typeof(T6)) };
            return this;
        }
        
        public EntityQuery Component<T1,T2,T3,T4,T5,T6,T7>() 
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
        {
            _getEntityFunc = (action, iterateAction)=>_world.GetComponentPool<T1>().IterateEntity(action, iterateAction);
            _need = new[] { ComponentsContainer.Get.ComponentIndex(typeof(T2)),
                ComponentsContainer.Get.ComponentIndex(typeof(T3)),
                ComponentsContainer.Get.ComponentIndex(typeof(T4)),
                ComponentsContainer.Get.ComponentIndex(typeof(T5)),
                ComponentsContainer.Get.ComponentIndex(typeof(T6)),
                ComponentsContainer.Get.ComponentIndex(typeof(T7)) };
            return this;
        }
        
        public EntityQuery Component<T1,T2,T3,T4,T5,T6,T7,T8>() 
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
            where T8 : struct, IComponent
        {
            _getEntityFunc = (action, iterateAction)=>_world.GetComponentPool<T1>().IterateEntity(action, iterateAction);
            _need = new[] { ComponentsContainer.Get.ComponentIndex(typeof(T2)),
                ComponentsContainer.Get.ComponentIndex(typeof(T3)),
                ComponentsContainer.Get.ComponentIndex(typeof(T4)),
                ComponentsContainer.Get.ComponentIndex(typeof(T5)),
                ComponentsContainer.Get.ComponentIndex(typeof(T6)),
                ComponentsContainer.Get.ComponentIndex(typeof(T7)),
                ComponentsContainer.Get.ComponentIndex(typeof(T8)) };
            return this;
        }
        
        public EntityQuery Component<T1,T2,T3,T4,T5,T6,T7,T8,T9>() 
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
            where T8 : struct, IComponent
            where T9 : struct, IComponent
        {
            _getEntityFunc = (action, iterateAction)=>_world.GetComponentPool<T1>().IterateEntity(action, iterateAction);
            _need = new[] { ComponentsContainer.Get.ComponentIndex(typeof(T2)),
                ComponentsContainer.Get.ComponentIndex(typeof(T3)),
                ComponentsContainer.Get.ComponentIndex(typeof(T4)),
                ComponentsContainer.Get.ComponentIndex(typeof(T5)),
                ComponentsContainer.Get.ComponentIndex(typeof(T6)),
                ComponentsContainer.Get.ComponentIndex(typeof(T7)),
                ComponentsContainer.Get.ComponentIndex(typeof(T8)),
                ComponentsContainer.Get.ComponentIndex(typeof(T9)) };
            return this;
        }
        
        public EntityQuery Component<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10>() 
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
            where T7 : struct, IComponent
            where T8 : struct, IComponent
            where T9 : struct, IComponent
            where T10 : struct, IComponent
        {
            _getEntityFunc = (action, iterateAction)=>_world.GetComponentPool<T1>().IterateEntity(action, iterateAction);
            _need = new[] { ComponentsContainer.Get.ComponentIndex(typeof(T2)),
                ComponentsContainer.Get.ComponentIndex(typeof(T3)),
                ComponentsContainer.Get.ComponentIndex(typeof(T4)),
                ComponentsContainer.Get.ComponentIndex(typeof(T5)),
                ComponentsContainer.Get.ComponentIndex(typeof(T6)),
                ComponentsContainer.Get.ComponentIndex(typeof(T7)),
                ComponentsContainer.Get.ComponentIndex(typeof(T8)),
                ComponentsContainer.Get.ComponentIndex(typeof(T9)),
                ComponentsContainer.Get.ComponentIndex(typeof(T10)) };
            return this;
        }
    }
    
    
    public partial class EntityQuery
    {
        public EntityQuery NotComponent<T1>() where T1 : IComponent
        {
            _skip = new[] { ComponentsContainer.Get.ComponentIndex(typeof(T1)) };
            return this;
        }
        
        public EntityQuery NotComponent<T1,T2>() 
            where T1 : IComponent
            where T2 : IComponent
        {
            _skip = new[] { ComponentsContainer.Get.ComponentIndex(typeof(T1)),
                ComponentsContainer.Get.ComponentIndex(typeof(T2)) };
            return this;
        }
        
        public EntityQuery NotComponent<T1,T2,T3>() 
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
        {
            _skip = new[] { ComponentsContainer.Get.ComponentIndex(typeof(T1)),
                ComponentsContainer.Get.ComponentIndex(typeof(T2)),
                ComponentsContainer.Get.ComponentIndex(typeof(T3)) };
            return this;
        }
        
        public EntityQuery NotComponent<T1,T2,T3,T4>() 
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
            where T4 : IComponent
        {
            _skip = new[] { ComponentsContainer.Get.ComponentIndex(typeof(T1)),
                ComponentsContainer.Get.ComponentIndex(typeof(T2)),
                ComponentsContainer.Get.ComponentIndex(typeof(T3)),
                ComponentsContainer.Get.ComponentIndex(typeof(T4)) };
            return this;
        }
        
        public EntityQuery NotComponent<T1,T2,T3,T4,T5>() 
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
            where T4 : IComponent
            where T5 : IComponent
        {
            _skip = new[] { ComponentsContainer.Get.ComponentIndex(typeof(T1)),
                ComponentsContainer.Get.ComponentIndex(typeof(T2)),
                ComponentsContainer.Get.ComponentIndex(typeof(T3)),
                ComponentsContainer.Get.ComponentIndex(typeof(T4)),
                ComponentsContainer.Get.ComponentIndex(typeof(T5)) };
            return this;
        }
        
        public EntityQuery NotComponent<T1,T2,T3,T4,T5,T6>() 
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
            where T4 : IComponent
            where T5 : IComponent
            where T6 : IComponent
        {
            _skip = new[] { ComponentsContainer.Get.ComponentIndex(typeof(T1)),
                ComponentsContainer.Get.ComponentIndex(typeof(T2)),
                ComponentsContainer.Get.ComponentIndex(typeof(T3)),
                ComponentsContainer.Get.ComponentIndex(typeof(T4)),
                ComponentsContainer.Get.ComponentIndex(typeof(T5)),
                ComponentsContainer.Get.ComponentIndex(typeof(T6)) };
            return this;
        }
        
        public EntityQuery NotComponent<T1,T2,T3,T4,T5,T6,T7>() 
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
            where T4 : IComponent
            where T5 : IComponent
            where T6 : IComponent
            where T7 : IComponent
        {
            _skip = new[] { ComponentsContainer.Get.ComponentIndex(typeof(T1)),
                ComponentsContainer.Get.ComponentIndex(typeof(T2)),
                ComponentsContainer.Get.ComponentIndex(typeof(T3)),
                ComponentsContainer.Get.ComponentIndex(typeof(T4)),
                ComponentsContainer.Get.ComponentIndex(typeof(T5)),
                ComponentsContainer.Get.ComponentIndex(typeof(T6)),
                ComponentsContainer.Get.ComponentIndex(typeof(T7))};
            return this;
        }
        
        public EntityQuery NotComponent<T1,T2,T3,T4,T5,T6,T7,T8>() 
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
            where T4 : IComponent
            where T5 : IComponent
            where T6 : IComponent
            where T7 : IComponent
            where T8 : IComponent
        {
            _skip = new[] { ComponentsContainer.Get.ComponentIndex(typeof(T1)),
                ComponentsContainer.Get.ComponentIndex(typeof(T2)),
                ComponentsContainer.Get.ComponentIndex(typeof(T3)),
                ComponentsContainer.Get.ComponentIndex(typeof(T4)),
                ComponentsContainer.Get.ComponentIndex(typeof(T5)),
                ComponentsContainer.Get.ComponentIndex(typeof(T6)),
                ComponentsContainer.Get.ComponentIndex(typeof(T7)),
                ComponentsContainer.Get.ComponentIndex(typeof(T8)) };
            return this;
        }
        
        public EntityQuery NotComponent<T1,T2,T3,T4,T5,T6,T7,T8,T9>() 
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
            where T4 : IComponent
            where T5 : IComponent
            where T6 : IComponent
            where T7 : IComponent
            where T8 : IComponent
            where T9 : IComponent
        {
            _skip = new[] { ComponentsContainer.Get.ComponentIndex(typeof(T1)),
                ComponentsContainer.Get.ComponentIndex(typeof(T2)),
                ComponentsContainer.Get.ComponentIndex(typeof(T3)),
                ComponentsContainer.Get.ComponentIndex(typeof(T4)),
                ComponentsContainer.Get.ComponentIndex(typeof(T5)),
                ComponentsContainer.Get.ComponentIndex(typeof(T6)),
                ComponentsContainer.Get.ComponentIndex(typeof(T7)),
                ComponentsContainer.Get.ComponentIndex(typeof(T8)),
                ComponentsContainer.Get.ComponentIndex(typeof(T9)) };
            return this;
        }
        
        public EntityQuery NotComponent<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10>() 
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
            where T4 : IComponent
            where T5 : IComponent
            where T6 : IComponent
            where T7 : IComponent
            where T8 : IComponent
            where T9 : IComponent
            where T10 : IComponent
        {
            _skip = new[] { ComponentsContainer.Get.ComponentIndex(typeof(T1)),
                ComponentsContainer.Get.ComponentIndex(typeof(T2)),
                ComponentsContainer.Get.ComponentIndex(typeof(T3)),
                ComponentsContainer.Get.ComponentIndex(typeof(T4)),
                ComponentsContainer.Get.ComponentIndex(typeof(T5)),
                ComponentsContainer.Get.ComponentIndex(typeof(T6)),
                ComponentsContainer.Get.ComponentIndex(typeof(T7)),
                ComponentsContainer.Get.ComponentIndex(typeof(T8)),
                ComponentsContainer.Get.ComponentIndex(typeof(T9)),
                ComponentsContainer.Get.ComponentIndex(typeof(T10)) };
            return this;
        }
    }
}