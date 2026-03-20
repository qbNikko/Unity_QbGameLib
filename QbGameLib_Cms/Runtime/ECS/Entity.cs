namespace QbGameLib.Cms.ECS
{
    public struct Entity
    {
        internal int _id;
        internal World _world;
        internal int[] _components;

        internal EntityMonoBehaviour _monoBehaviour;
        
        public int ID => _id;
        
        public World World => _world;

        public EntityMonoBehaviour MonoBehaviour => _monoBehaviour;

        public ref T GetComponent<T>() where T : struct, IComponent
        {
            return ref _world.GetComponentPool<T>().Get(this);
        }
        
        public void RemoveComponent<T>() where T : struct, IComponent
        {
            _world.GetComponentPool<T>().Remove(ref this);
        }
        
        public void AddComponent<T>(ref T component) where T : struct, IComponent
        {
            _world.GetComponentPool<T>().Insert(ref this, ref component);
        }

        public Entity(int id, World world) : this()
        {
            _id = id;
            _world = world;
            _components = new int[ComponentsContainer.Get.Components.Length];
            for (var i = 0; i < _components.Length; i++)
            {
                _components[i] = -1;
            }
        }
    }
}