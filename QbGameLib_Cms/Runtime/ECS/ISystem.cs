namespace QbGameLib.Cms.ECS
{
    public interface ISystem
    {
        public void InitQuery(World world, EntityQuery query);
    }
    public interface IUpdateSystem : ISystem
    {
        public void Update();
    }
    public interface IFixedUpdateSystem : ISystem
    {
        public void Update();
    }
    
    public interface ITaskUpdateSystem : ISystem
    {
        public void Update();
    }
    
    public interface IInitSystem : ISystem
    {
        public void Init(World world);
    }
    
    public interface IPostInitSystem : ISystem
    {
        public void PostInit();
    }
}