using System;
using Reflex.Attributes;
using Reflex.Core;
using Reflex.Extensions;
using SpaceTraveler.Scripts.Managers;
using SpaceTraveler.Scripts.StateMachine.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceTraveler.Scripts.StateMachine
{
    public class GameStateMachine : MonoBehaviour, IGameStateMachine
    {
        private Container _container;
        private GameState _currentState;
        private GameState _previousState;

        [Inject] private UIManager _uiManager;
        [Inject] private InputManager _inputManager;
        [Inject] private LevelManager _levelManager;
        [Inject] private AudioManager _audioManager;

        public Type CurrentStateType => _currentState?.GetType();

        private Container Container
        {
            get
            {
                if (_container == null)
                {
                    CreateStates();
                }

                return _container;
            }
        }

        public static GameStateMachine Create()
        {
            var gameStateMachine = new GameObject(nameof(GameStateMachine)).AddComponent<GameStateMachine>();
            DontDestroyOnLoad(gameStateMachine.gameObject);

            return gameStateMachine;
        }

        private void OnDestroy()
        {
            _currentState?.Exit();
            _container?.Dispose();
        }

        public void ChangeState<T>() where T : GameState
        {
            var state = Container.Resolve<T>();

            ChangeState(state);
        }

        public void ReturnToPreviousState()
        {
            if (_previousState != null)
            {
                ChangeState(_previousState);
            }
        }

        private void ChangeState(GameState state)
        {
            if (_currentState != null)
            {
                if (_currentState == state) return;

                _previousState = _currentState;
                _currentState.Exit();
            }

            _currentState = state;
            _currentState.Enter();

            Debug.Log($"State changed to: {state.GetType().Name}");
        }


        private void CreateStates()
        {
            var fsmContainer = new ContainerBuilder();
            var sceneContainer = SceneManager.GetActiveScene().GetSceneContainer();
            fsmContainer.SetParent(sceneContainer);

            fsmContainer.AddSingleton(typeof(MenuState));
            fsmContainer.AddSingleton(typeof(PlayingState));
            fsmContainer.AddSingleton(typeof(PausedState));
            fsmContainer.AddSingleton(typeof(GameOverState));
            fsmContainer.AddSingleton(typeof(LevelCompleteState));
            fsmContainer.AddSingleton(typeof(LoadingState));
            fsmContainer.AddSingleton(typeof(ExitState));

            _container = fsmContainer.Build();
        }
    }
}