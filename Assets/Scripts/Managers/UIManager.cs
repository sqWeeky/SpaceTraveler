using Reflex.Extensions;
using System.Collections.Generic;
using UnityEngine;
using System;
using Windows;
using Configs;
using Reflex.Extensions;
using Reflex.Injectors;

namespace Managers
{
    public class UIManager : BaseManager<UIManager>
    {
        private List<BaseWindow> _openedWindows;
        private Dictionary<Type, BaseWindow> _cachedWindows;

        private Canvas _mainCanvas;
        private List<BaseWindow> _windows;

        private void Awake()
        {
            _openedWindows = new List<BaseWindow>();
            _cachedWindows = new Dictionary<Type, BaseWindow>();
        }

        public void Init(UIManagerConfig config)
        {
            _windows = new List<BaseWindow>();

            foreach (BaseWindow window in config.BaseWindows)
                _windows.Add(window);
        }

        public BaseWindow OpenWindow<T>() where T : BaseWindow
        {
            if (_mainCanvas == null)
                _mainCanvas = GameObject.FindGameObjectWithTag(Constants.GameTags.MainCanvasTag).GetComponent<Canvas>();

            DontDestroyOnLoad(_mainCanvas.gameObject);

            BaseWindow window = GetOrCreateWindow<T>();

            if (window == null) return null;

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

        public BaseWindow GetOrCreateWindow<T>() where T : BaseWindow
        {
            T window = GetWindowByType<T>();

            if (window == null)
                window = CreateWindow<T>();

            return window;
        }

        private T CreateWindow<T>() where T : BaseWindow
        {
            Debug.Log($"IUManager create => {typeof(T).Name}");
            GameObject prefab = FindWindowPrefab<T>();

            // GameObject prefab = Resources.Load<GameObject>($"Windows/{typeof(T).Name}");

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

                AttributeInjector.Inject(windowComponent, gameObject.scene.GetSceneContainer());
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

            BaseWindow newWindow = Instantiate(windowPrefab, _mainCanvas.transform);
            AttributeInjector.Inject(newWindow,
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetSceneContainer());
            newWindow.gameObject.SetActive(false);
            _cachedWindows[type] = newWindow;

            return (T)newWindow;
        }
    }
}