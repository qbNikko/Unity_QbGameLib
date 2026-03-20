using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace QbGameLib.Cms.ECS
{
    public class ComponentsContainer
    {
        private static ComponentsContainer _container;

        public static ComponentsContainer Get
        {
            get
            {
                if (_container == null) _container = new ComponentsContainer();
                return _container;
            }
        }
        
        private Type[] _components;

        public Type[] Components => _components;

        public int ComponentIndex(Type type)
        {
            for (var i = 0; i < _components.Length; i++)
            {
                if(type.Equals(_components[i])) return i;
            }
            return -1;
        }

        internal ComponentsContainer()
        {
            Type type = typeof(IComponent);
            _components = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) 
                            && p.IsValueType 
                            && !p.IsEnum 
                            && p != type)
                .ToArray();
        }
    }
    
    public class EntityComponentPool<T> where T : struct, IComponent
    {
        internal static EntityComponentPool<T>[] _pools = new EntityComponentPool<T>[1];
        
        internal static ref EntityComponentPool<T> GetPool(World world)
        {
            int worldIndex = world.Index;
            if (_pools.Length < worldIndex) Array.Resize (ref _pools, worldIndex+1);
            if (_pools[worldIndex] == null)
            {
                Type[] components = ComponentsContainer.Get.Components;
                Type type = typeof(T);
                int index = -1;
                for (var i = 0; i < components.Length; i++) {
                    if (type.Equals(components[i]))
                    {
                        index = i;
                        break;
                    }
                }
                if (index == -1) throw new Exception($"Component <{typeof(T).Name}> not found");
                _pools[worldIndex] = new EntityComponentPool<T>(world, index, world.EntityCountPool,world.EntityCountPool*2);
            }
            return ref _pools[worldIndex];
        }

        private World _world;
        private int _id;
        private int[] _entities;
        private T[] _components;
        /**
         * ссылка на компонент
         */
        private int[] _links;
        private int _lenEntities;
        private int _lenComponents;

        public int[] Entities => _entities;
        private int _offset = 0;
        private bool _iterate = false;
        
        internal void IterateEntity(EntityIndexActionRef iterateAction, EntityQueryActionRef query)
        {
            _iterate = true;
            for (var i = 0; i < _lenEntities; i++)
            {
                iterateAction.Invoke(_entities[i], _world, query);
                i += _offset;
                _offset = 0;
            }
            _iterate = false;
        }

        public EntityComponentPool(World world, int id,
            int entityCount = 512, 
            int componentCount = 256
            )
        {
            _id = id;
            _world = world;
            if (_pools.Length < _world.Index) Array.Resize (ref _pools, _world.Index+1);
            _pools[world.Index] = this;
            _entities =  new int[entityCount];
            _components = new T[componentCount];
            _links = new int[entityCount];
            _lenEntities = 0;
            _lenComponents = 0;
        }
        
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ref T Add(ref Entity entity)
        {
            if (entity._components.Length < _id + 1) Array.Resize (ref entity._components, (_id+1));
            if (entity._components[_id] != -1) return ref _components[_links[entity._components[_id]]];
            if (_components.Length == _lenComponents) Array.Resize (ref _components, _lenComponents << 1);
            if (_entities.Length == _lenEntities)
            {
                Array.Resize (ref _entities, _lenEntities << 1);
                Array.Resize (ref _links, _lenEntities << 1);
            }
            _entities[_lenEntities] =  entity.ID;
            _components[_lenComponents] =  new T();
            _links[_lenEntities] = _lenComponents;
            entity._components[_id] = _lenEntities;
            _lenEntities++;
            _lenComponents++;
            return ref _components[_lenComponents-1];
        }
        
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool Insert(ref Entity entity, ref T component)
        {
            if (entity._components.Length < _id + 1) Array.Resize (ref entity._components, (_id+1));
            if (entity._components[_id] != -1) return false;
            if (_components.Length == _lenComponents) Array.Resize (ref _components, _lenComponents << 1);
            if (_entities.Length == _lenEntities)
            {
                Array.Resize (ref _entities, _lenEntities << 1);
                Array.Resize (ref _links, _lenEntities << 1);
            }
            _entities[_lenEntities] =  entity.ID;
            _components[_lenComponents] =  component;
            _links[_lenEntities] = _lenComponents;
            entity._components[_id] = _lenEntities;
            _lenEntities++;
            _lenComponents++;
            return true;
        }
        
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool Remove(ref Entity entity)
        {
            int entityIndex = entity._components[_id];
            if (entityIndex == -1) return false;
            entity._components[_id] = -1;
            int componentIndex = _links[entityIndex];
            _lenComponents--;
            _lenEntities--;
            (_components[componentIndex], _components[_lenComponents]) = (_components[_lenComponents], _components[componentIndex]);
            (_entities[entityIndex], _entities[_lenEntities]) = (_entities[_lenEntities], _entities[entityIndex]);
            (_links[entityIndex], _links[_lenEntities]) = (_links[_lenEntities], _links[entityIndex]);
            _world.GetEntity(_entities[entityIndex])._components[_id]=entityIndex;
            _links[entityIndex] = componentIndex;
            if(_iterate) _offset = -1;
            return true;
        }
        
        public ref T Get(Entity entity)
        {
            int entityIndex = entity._components[_id];
            return ref _components[_links[entityIndex]];
        }
    }
}