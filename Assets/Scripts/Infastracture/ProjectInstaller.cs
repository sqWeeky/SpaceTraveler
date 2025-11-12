using Configs;
using Managers;
using Players;
using Reflex.Core;
using StateMachine;
using StateMachine.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infastracture
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        [Header("GameConfig")] [SerializeField]
        private GameConfig _gameConfig;

        [Header("Core Systems")] [SerializeField]
        private GameStateMachine _gameStateMachine;

        [SerializeField] private Player _player;

        [Header("Managers")] [SerializeField] private UIManager _uiManager;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private LevelManager _levelManager;

        public void InstallBindings(ContainerBuilder builder)
        {
            Debug.LogError("Вошел в билдер");
            // === ScriptableObjects ===
            builder.AddSingleton(_gameConfig);

            // === Core Game Systems ===
            //builder.AddSingleton(_gameStateMachine);
            //_gameStateMachine.InitializeStates();   
            builder.AddSingleton(_player);
            builder.AddSingleton<GameStateMachine>(container => container.Construct<GameStateMachine>());

            // === Managers ===
            builder.AddSingleton(_audioManager);
            builder.AddSingleton(_uiManager);
            builder.AddSingleton(_inputManager);
            builder.AddSingleton(_levelManager);
            

            // === Game States ===
            builder.AddTransient<MenuState>(container => container.Construct<MenuState>());
            builder.AddTransient<PlayingState>(container => container.Construct<PlayingState>());
            builder.AddTransient<PausedState>(container => container.Construct<PausedState>());
            builder.AddTransient<GameOverState>(container => container.Construct<GameOverState>());
            builder.AddTransient<LevelCompleteState>(container => container.Construct<LevelCompleteState>());
            builder.AddTransient<LoadingState>(container => container.Construct<LoadingState>());
            builder.AddTransient<ExitState>(container => container.Construct<ExitState>());

            Debug.Log("ProjectInstaller: All dependencies registered");

            //SceneManager.LoadScene("MainMenu");
            //_gameStateMachine.ChangeState<MenuState>();
        }
    }
}