using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using QbGameLib.Pool;
using QbGameLib.Pool.Collection;
using UnityEngine;
using UnityEngine.Pool;

namespace QbGameLib.Cms.ECS
{
    public class World
    {
        private static World[] _instance;
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Reset()=> _instance = null;

        public static World Get(int index)
        {
            if (_instance == null || _instance.Length<index) return null;
            return _instance[index];
        }

        private int _index;
        private int _entityCountPool;

        internal int EntityCountPool => _entityCountPool;
        internal int Index => _index;
        
        
        private IList<Type> _allSystemsType = new FastList<Type>();
        private FastList<ISystem> _allSystems = new FastList<ISystem>();
        private FastList<IInitSystem> _initSystems = new FastList<IInitSystem>();
        private FastList<ITaskUpdateSystem> _taskSystems = new FastList<ITaskUpdateSystem>();
        private FastList<IUpdateSystem> _updateSystems = new FastList<IUpdateSystem>();
        private FastList<IFixedUpdateSystem>  _fixedUpdateSystems = new FastList<IFixedUpdateSystem>();
        private FastList<Entity> _entities;

        
        private bool _queryCacheInitialize = false;
        private bool _queryCacheRefresh = false;

        
        public FastList<Entity> Entities => _entities;
        

        public World(int index=0, 
            int entityCount = 512, 
            int componentCount = 256)
        {
            Debug.Log("Creating world " + index);
            if (_instance == null) _instance = new World[index + 1];
            _instance[index] = this;
            _entityCountPool = entityCount;
            _index =  index;
            _entities = new FastList<Entity>(cap: entityCount);
        }
        
        
        
        public EntityComponentPool<T> GetComponentPool<T>() where T : struct, IComponent
        {
            return EntityComponentPool<T>.GetPool(this);
        }
        
        public ref T AddComponent<T>(ref Entity entity) where T : struct, IComponent
        {
            return ref EntityComponentPool<T>.GetPool(this).Add(ref entity);
        }
        
        public World InsertComponent<T>(ref Entity entity, ref T component) where T : struct, IComponent
        {
            EntityComponentPool<T>.GetPool(this).Insert(ref entity, ref component);
            return this;
        }

        /**
         * Добавление системы
         */
        public World AddSystem(ISystem system)
        {
            if(_allSystemsType.Contains(system.GetType())) throw new ArgumentException($"System {system.GetType()} already exists");
            _allSystemsType.Add(system.GetType());
            Reflection.Reflection.InjectIfExists(system, "_world", this);
            EntityQuery entityQuery = new EntityQuery(this);
            system.InitQuery(this, entityQuery);
            _allSystems.Add(system);
            if(system is IFixedUpdateSystem fs) _fixedUpdateSystems.Add(fs);
            if(system is ITaskUpdateSystem ts) _taskSystems.Add(ts);
            if(system is IUpdateSystem us) _updateSystems.Add(us);
            if(system is IInitSystem ins) _initSystems.Add(ins);
            return this;
        }

        public World CreateQuery(out EntityQuery query)
        {
            query = new EntityQuery(this);
            return this;
        }
        
        /**
         * Добавление системы
         */
        public T AddSystem<T>() where T : ISystem, new()
        {
            T system = new T();
            AddSystem(system);
            return system;
        }
        
        /**
         * Удаление системы
         */
        public void RemoveSystem(ISystem system)
        {
            _allSystems.Remove(system);
            if(system is IFixedUpdateSystem fs) _fixedUpdateSystems.Remove(fs);
            if(system is ITaskUpdateSystem ts) _taskSystems.Remove(ts);
            if(system is IUpdateSystem us) _updateSystems.Remove(us);
            if(system is IInitSystem ins) _initSystems.Remove(ins);
        }

        /**
         * Добавление сущьности в мир
         */
        public ref Entity CreateEntity()
        {
            Entity entity = new Entity(_entities.Count, this);
            _entities.Add(ref entity);
            return ref _entities.Get(entity.ID);
        }

        /**
         * Добавление сущьности в мир
         */
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ref Entity GetEntity(int index)
        {
            return ref _entities.Get(index);
        }
        
        /**
         * Удаление сущьности из мира
         */
        public void RemoveEntity(int index)
        {
            ref Entity entity = ref _entities.Get(index);
            
            
            if (_entities.RemoveAtAndCheckMove(index))
                _entities.Get(index)._id = index;
        }
        
        public void Init()
        {
            if(_initSystems.Count==0) return;
            foreach (IInitSystem system in _initSystems)
                system.Init(this);
        }

        public void FixedUpdate()
        {
            foreach (IFixedUpdateSystem system in _fixedUpdateSystems)
                system.Update();
        }
        
        public void Update()
        {
            for (int i = 0; i < _updateSystems.Count; i++)
            { 
                _updateSystems.Get(i).Update();
            }

        }
    }
    
}