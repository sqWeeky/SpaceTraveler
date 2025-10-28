namespace GameRoot
{
    public interface IGameRoot
    {
        T GetService<T>() where T : class;
        void RegisterService<T>(T service) where T : class;
        void UnregisterService<T>() where T : class;
        bool HasService<T>() where T : class;
    }
}