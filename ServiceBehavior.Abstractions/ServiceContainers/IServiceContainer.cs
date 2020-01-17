namespace ServiceBehavior.Abstractions
{
    public interface IServiceContainer
    {
        void Clear();
        T GetService<T>() where T : class;
        T GetService<T>(string key) where T : class;
        void RegisterService(object service);
        void RegisterService(string key, object service);
    }
}