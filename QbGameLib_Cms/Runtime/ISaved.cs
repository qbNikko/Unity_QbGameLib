namespace QbGameLib.Cms
{
    public interface ISaved<T>
    {
        public void Save(T data);
        
        public void Load(T data);
    }
}