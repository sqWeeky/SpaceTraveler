using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameRoot
{
    public class GameRoot : MonoBehaviour, IGameRoot
    {
        private static GameRoot _instance;
        private Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static IGameRoot Current => _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);

            // Саморегистрация
            RegisterService<IGameRoot>(this);
        }

        public void RegisterService<T>(T service) where T : class
        {
            Type serviceType = typeof(T);

            if (_services.ContainsKey(serviceType))
            {
                Debug.LogWarning($"Service {serviceType.Name} is already registered. Overwriting...");
                _services[serviceType] = service;
            }
            else
            {
                _services.Add(serviceType, service);
                Debug.Log($"Service registered: {serviceType.Name}");
            }
        }

        public T GetService<T>() where T : class
        {
            Type serviceType = typeof(T);

            if (_services.TryGetValue(serviceType, out object service))
            {
                return service as T;
            }

            // Попробуем найти по наследованию
            foreach (var kvp in _services)
            {
                if (serviceType.IsAssignableFrom(kvp.Key))
                {
                    return kvp.Value as T;
                }
            }

            Debug.LogError($"Service {serviceType.Name} not found!");
            return null;
        }

        public void UnregisterService<T>() where T : class
        {
            Type serviceType = typeof(T);

            if (_services.ContainsKey(serviceType))
            {
                _services.Remove(serviceType);
                Debug.Log($"Service unregistered: {serviceType.Name}");
            }
        }

        public bool HasService<T>() where T : class
        {
            Type serviceType = typeof(T);

            if (_services.ContainsKey(serviceType))
                return true;

            // Проверяем по наследованию
            foreach (var type in _services.Keys)
            {
                if (serviceType.IsAssignableFrom(type))
                    return true;
            }

            return false;
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _services.Clear();
                _instance = null;
            }
        }
    }
}