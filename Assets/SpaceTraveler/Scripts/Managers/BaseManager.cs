using UnityEngine;

namespace SpaceTraveler.Scripts.Managers
{
    public abstract class BaseManager<T> : MonoBehaviour, IManager
        where T : BaseManager<T>
    {
        public static T Create()
        {
            var manager = new GameObject(typeof(T).Name).AddComponent<T>();
            DontDestroyOnLoad(manager.gameObject);

           return manager;
        }

        public virtual void InitManager()
        {
            return;
        }
    }
}