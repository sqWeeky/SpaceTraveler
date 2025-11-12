using System.Collections.Generic;
using UnityEngine;
using System;
using Configs;
using Reflex.Attributes;
using Reflex.Core;
using StateMachine;

namespace Managers
{
    public class UIManager : BaseManager
    {
        [SerializeField] private List<BaseWindow> _windows;
        [SerializeField] private Canvas _mainCanvas;

        private readonly List<BaseWindow> _openedWindows = new();
        private readonly Dictionary<Type, BaseWindow> _cachedWindows = new();

        // private GameStateMachine _gameStateMachine;
        // private AudioManager _audioManager;

        public BaseWindow OpenWindow<T>() where T : BaseWindow
        {
            if (_mainCanvas == null)
                _mainCanvas = GameObject.FindGameObjectWithTag(Constants.GameTags.MainCanvasTag).GetComponent<Canvas>();

            DontDestroyOnLoad(_mainCanvas.gameObject);

            BaseWindow window = GetOrCreateWindow<T>();

            if (window == null) return null;

            if (_container == null)
            {
                Debug.LogError("Container is null in UIManager! DI not initialized properly.");
                return window;
            }

            window.InjectDependencies(
                _container.Resolve<GameStateMachine>(), 
                this, 
                _container.Resolve<AudioManager>());

            if (!_openedWindows.Contains(window))
            {
                window.gameObject.SetActive(true);
                _openedWindows.Add(window);
            }

            Debug.Log($"Opened window: {typeof(T).Name}");

            return window;
        }

        public void CloseWindow<T>() where T : BaseWindow
        {
            for (int i = 0; i < _openedWindows.Count; i++)
            {
                if (_openedWindows[i] is T window)
                {
                    window.gameObject.SetActive(false);
                    _openedWindows.RemoveAt(i);
                    return;
                }
            }
        }

        public void CloseAllWindows()
        {
            foreach (BaseWindow window in _openedWindows)
            {
                if (window != null)
                    window.gameObject.SetActive(false);
            }

            _openedWindows.Clear();
        }

        private void RegisterWindow(BaseWindow window)
        {
            if (window == null || _windows.Contains(window)) return;

            window.transform.SetParent(_mainCanvas.transform, false);
            _windows.Add(window);
            _cachedWindows[window.GetType()] = window;
        }

        private BaseWindow GetOrCreateWindow<T>() where T : BaseWindow
        {
            T window = GetWindowByType<T>();

            if (window == null)
                window = CreateWindow<T>();

            return window;
        }

        private T CreateWindow<T>() where T : BaseWindow
        {
            GameObject prefab = FindWindowPrefab<T>();

            if (prefab == null)
            {
                Debug.LogError($"Window prefab of type '{typeof(T)}' not found.");
                return null;
            }

            GameObject windowInstance = Instantiate(prefab, _mainCanvas.transform);
            T windowComponent = windowInstance.GetComponent<T>();

            if (windowComponent != null)
            {
                windowComponent.gameObject.SetActive(false);
                RegisterWindow(windowComponent);
            }

            return windowComponent;
        }

        private GameObject FindWindowPrefab<T>() where T : BaseWindow =>
            _windows.Find(w => w.GetType() == typeof(T))?.gameObject;

        private T GetWindowByType<T>() where T : BaseWindow
        {
            Type type = typeof(T);

            if (_cachedWindows.TryGetValue(type, out BaseWindow cachedWindow))
                return (T)cachedWindow;

            BaseWindow windowPrefab = _windows.Find(w => w.GetType() == type);
            if (windowPrefab == null)
            {
                Debug.LogError($"Window of type '{type}' not found.");
                return null;
            }

            var newWindow = Instantiate(windowPrefab, _mainCanvas.transform);
            newWindow.gameObject.SetActive(false);
            _cachedWindows[type] = newWindow;

            return (T)newWindow;
        }
    }
}