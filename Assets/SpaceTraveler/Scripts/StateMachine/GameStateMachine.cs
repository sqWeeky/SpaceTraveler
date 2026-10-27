using System;
using System.Collections.Generic;
using Reflex.Attributes;
using Reflex.Extensions;
using Reflex.Injectors;
using SpaceTraveler.Scripts.Managers;
using SpaceTraveler.Scripts.StateMachine.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceTraveler.Scripts.StateMachine
{
    public class GameStateMachine : MonoBehaviour, IGameStateMachine
    {
        [Inject] private UIManager _uiManager;
        [Inject] private InputManager _inputManager;
        [Inject] private LevelManager _levelManager;
        [Inject] private AudioManager _audioManager;
        [Inject] private SaveManager _saveManager;

        //private Container _container;
        private static Dictionary<Type, GameState> _states = new();

        private GameState _currentState;
        private GameState _previousState;

        public GameState CurrentState => _currentState;

        // private Container Container
        // {
        //     get
        //     {
        //         if (_container == null)
        //         {
        //             CreateStates();
        //         }
        //
        //         return _container;
        //     }
        // }

        public static GameStateMachine Create()
        {
            var gameStateMachine = new GameObject(nameof(GameStateMachine)).AddComponent<GameStateMachine>();
            DontDestroyOnLoad(gameStateMachine.gameObject);

            return gameStateMachine;
        }

        private void Start()
        {
            GameObjectInjector.InjectSingle(gameObject, SceneManager.GetActiveScene().GetSceneContainer());

            RegisterState(new GameOverState().Init(_uiManager, _audioManager, _inputManager, _levelManager,
                _saveManager));
            RegisterState(new LevelCompleteState().Init(_uiManager, _audioManager, _inputManager, _levelManager,
                _saveManager));
            RegisterState(new PausedState().Init(_uiManager, _audioManager, _inputManager, _levelManager,
                _saveManager));
            RegisterState(
                new PlayingState().Init(_uiManager, _audioManager, _inputManager, _levelManager, _saveManager));
            RegisterState(new MenuState().Init(_uiManager, _audioManager, _inputManager, _levelManager, _saveManager));
        }

        private void OnDestroy()
        {
            _currentState?.Exit();
            //_container?.Dispose();
        }

        public void ExitState<T>() where T : GameState
        {
            Type type = typeof(T);

            if (!_states.TryGetValue(type, out var state))
            {
                Debug.LogError($"State {type.Name} not registered in GameStateMachine");
                return;
            }

            if (_currentState != null && _currentState == state)
            {
                _previousState = _currentState;
                _currentState.Exit();
                _currentState = null;
            }
        }

        public void ChangeState<T>() where T : GameState
        {
            Type type = typeof(T);

            if (!_states.TryGetValue(type, out var state))
            {
                Debug.LogError($"State {type.Name} not registered in GameStateMachine");
                return;
            }

            if (_currentState != null)
            {
                if (_currentState == state) return;

                _previousState = _currentState;
                _currentState.Exit();
                Debug.LogError($"State - exit");
            }

            _currentState = state;
            _currentState.Enter();

            Debug.Log($"State changed to: {state.GetType().Name}");

            //var state = Container.Resolve<T>();
            //ChangeState(state);
        }

        private void RegisterState(GameState state)
        {
            _states[state.GetType()] = state;
        }

        // private void ChangeState(GameState state)
        // {
        //     if (_currentState != null)
        //     {
        //         if (_currentState == state) return;
        //
        //         _previousState = _currentState;
        //         _currentState.Exit();
        //         Debug.LogError($"State - exit");
        //     }
        //
        //     _currentState = state;
        //     _currentState.Enter();
        //
        //     Debug.Log($"State changed to: {state.GetType().Name}");
        // }


        // private void CreateStates()
        // {
        //     var fsmContainer = new ContainerBuilder();
        //     var sceneContainer = SceneManager.GetActiveScene().GetSceneContainer();
        //     fsmContainer.SetParent(sceneContainer);
        //
        //     fsmContainer.AddSingleton(typeof(MenuState));
        //     fsmContainer.AddSingleton(typeof(PlayingState));
        //     fsmContainer.AddSingleton(typeof(PausedState));
        //     fsmContainer.AddSingleton(typeof(GameOverState));
        //     fsmContainer.AddSingleton(typeof(LevelCompleteState));
        //     fsmContainer.AddSingleton(typeof(LoadingState));
        //     fsmContainer.AddSingleton(typeof(ExitState));
        //
        //     _container = fsmContainer.Build();
        // }
    }
}