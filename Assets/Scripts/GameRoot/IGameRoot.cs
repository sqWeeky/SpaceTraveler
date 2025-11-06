using Managers;

namespace GameRoot
{
    public interface IGameRoot
    {
        T GetManager<T>() where T : class;
        void RegisterManager<T>(BaseManager manager) where T : class;
        void UnregisterManager<T>() where T : class;
        bool HasManager<T>() where T : class;
    }
}