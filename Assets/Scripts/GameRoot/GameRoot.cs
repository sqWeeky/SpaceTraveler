using System;
using System.Collections.Generic;
using Configs;
using Managers;
using StateMachine;
using UnityEngine;

namespace GameRoot
{
    public class GameRoot : MonoBehaviour, IGameRoot
    {
        public static GameRoot Instance { get; private set; }

        [SerializeField] private GameConfig _config;
        [SerializeField] private GameStateMachine _gameStateMachine;
        [SerializeField] private List<BaseManager> _startManagers = new List<BaseManager>();

        private Dictionary<Type, BaseManager> _managers = new Dictionary<Type, BaseManager>();

        public static GameConfig GameConfig => Instance._config;
        public static GameStateMachine GameStateMachine => Instance._gameStateMachine;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Саморегистрация
            //RegisterManager<IGameRoot>(this);
            _gameStateMachine.Initialize(_config);
        }

        private void Start()
        {
            foreach (var manager in _startManagers)
            {
                if (manager != null)
                {
                    Type type = manager.GetType();

                    var loadedManager = LoadManager(manager, type);
                    loadedManager.InitManager();
                }
            }
        }

        public void RegisterManager<T>(BaseManager manager) where T : class
        {
            Type type = typeof(T);

            if (!_managers.TryAdd(type, manager))
            {
                Debug.LogWarning($"Service {type.Name} is already registered. Overwriting...");
                _managers[type] = manager;
            }
            else
            {
                Debug.Log($"Service registered: {type.Name}");
            }
        }

        public T GetManager<T>() where T : class
        {
            Type serviceType = typeof(T);

            if (_managers.TryGetValue(serviceType, out BaseManager service))
            {
                return service as T;
            }

            // Попробуем найти по наследованию
            foreach (var kvp in _managers)
            {
                if (serviceType.IsAssignableFrom(kvp.Key))
                {
                    return kvp.Value as T;
                }
            }

            Debug.LogError($"Service {serviceType.Name} not found!");
            return null;
        }

       public void UnregisterManager<T>() where T : class
        {
            Type serviceType = typeof(T);

            if (_managers.ContainsKey(serviceType))
            {
                _managers.Remove(serviceType);
                Debug.Log($"Service unregistered: {serviceType.Name}");
            }
        }

        public bool HasManager<T>() where T : class
        {
            Type serviceType = typeof(T);

            if (_managers.ContainsKey(serviceType))
                return true;

            // Проверяем по наследованию
            foreach (var type in _managers.Keys)
            {
                if (serviceType.IsAssignableFrom(type))
                    return true;
            }

            return false;
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                _managers.Clear();
                Instance = null;
            }
        }

        private static BaseManager LoadManager(BaseManager manager, Type type)
        {
            if (manager != null)
            {
                var instantiatedManager = Instantiate(manager, Instance.transform, true);
                Instance._managers[type] = instantiatedManager;

                Debug.Log($"Created {type.Name} by request");

                return instantiatedManager;
            }
            else
            {
                Debug.Log($"Manager {type.Name} not found in ManagersPrefabs folder");

                return null;
            }
        }
    }
}