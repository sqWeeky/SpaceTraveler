using Configs;
using Managers;
using Players;
using UnityEngine;
using Reflex.Core;
using StateMachine;
using StateMachine.States;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameInstaller : MonoBehaviour, IInstaller
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
           builder.AddSingleton(_gameConfig);

            builder.AddSingleton(_uiManager);
            builder.AddSingleton(_audioManager);
            builder.AddSingleton(_inputManager);
            builder.AddSingleton(_levelManager);

            builder.AddSingleton(_gameStateMachine);
            builder.AddSingleton<IGameStateMachine>(container => _gameStateMachine);
            builder.AddSingleton(_player);

            builder.AddTransient<MenuState>(container => CreateState<MenuState>(container));
            builder.AddTransient<PlayingState>(container => CreateState<PlayingState>(container));
            builder.AddTransient<PausedState>(container => CreateState<PausedState>(container));
            builder.AddTransient<GameOverState>(container => CreateState<GameOverState>(container));
            builder.AddTransient<LevelCompleteState>(container => CreateState<LevelCompleteState>(container));
            builder.AddTransient<LoadingState>(container => CreateState<LoadingState>(container));
            builder.AddTransient<ExitState>(container => CreateState<ExitState>(container));

            Debug.Log("GameInstaller: All bindings registered");
            SceneManager.LoadScene("MainMenu");
        }

        private T CreateState<T>(Container container) where T : GameState
        {
            return container.Construct<T>();
        }
    }
}