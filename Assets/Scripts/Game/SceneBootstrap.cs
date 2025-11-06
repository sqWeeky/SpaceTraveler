using Configs;
using Managers;
using StateMachine;
using UnityEngine;
using Reflex.Core;
using UnityEngine.SceneManagement;

namespace Game
{
    public class SceneBootstrap : MonoBehaviour
    {
        //private SceneBootstrap Instance { get; set; }

        [SerializeField] private GameInstaller _gameInstaller;

        private void Awake()
        {
            // if (Instance == null)
            // {
            //     Instance = this;
            //     DontDestroyOnLoad(gameObject);
            //
            //     if (_gameInstaller == null)
            //     {
            //         Debug.LogError("GameInstaller not assigned!");
            //         return;
            //     }
            //
            //     DontDestroyOnLoad(_gameInstaller.gameObject);
            // }
            // else
            // {
            //     Destroy(gameObject);
            // }

            var builder = new ContainerBuilder().SetName("GameContainer");

            _gameInstaller.InstallBindings(builder);

            Container container = builder.Build();

            InjectDependenciesIntoManagers(container);

            var stateMachine = container.Resolve<GameStateMachine>();
            var config = container.Resolve<GameConfig>();

            stateMachine.Initialize(config, container);

            InitializeManagers(container);

            Debug.Log("SceneBootstrap: Reflex DI Container initialized successfully!");

            SceneManager.LoadScene("MainMenu");
        }

        private void InjectDependenciesIntoManagers(Container container)
        {
            UIManager uiManager = container.Resolve<UIManager>();
            AudioManager audioManager = container.Resolve<AudioManager>();
            InputManager inputManager = container.Resolve<InputManager>();
            LevelManager levelManager = container.Resolve<LevelManager>();

            if (uiManager is IInjectContainer injectableUI)
                injectableUI.SetContainer(container);

            SetPrivateField(uiManager, "_container", container);
        }

        private void SetPrivateField(object obj, string fieldName, object value)
        {
            var field = obj.GetType().GetField(fieldName,
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            field?.SetValue(obj, value);
        }

        private void InitializeManagers(Container container)
        {
            container.Resolve<UIManager>().Construct(container);
            container.Resolve<AudioManager>().Construct(container);
            container.Resolve<InputManager>().Construct(container);
            container.Resolve<LevelManager>().Construct(container);
            //container.Resolve<LevelManager>().InitManager();

            Debug.Log("SceneBootstrap: All managers initialized");
        }
    }
}