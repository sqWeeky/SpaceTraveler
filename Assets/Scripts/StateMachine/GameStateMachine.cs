using System;
using System.Collections.Generic;
using Configs;
using StateMachine.States;
using UnityEngine;

namespace StateMachine
{
    public class GameStateMachine : MonoBehaviour, IGameStateMachine
    {
        [SerializeField] private GameConfig _config;

        private Dictionary<Type, GameState> _states;
        private GameState _currentState;
        private GameState _previousState;

        private void Awake()
        {
            _states = new Dictionary<Type, GameState>
            {
                { typeof(MenuState), new MenuState(this, _config) },
                { typeof(PlayingState), new PlayingState(this, _config) },
                { typeof(PausedState), new PausedState(this, _config) },
                { typeof(GameOverState), new GameOverState(this, _config) },
                { typeof(LevelCompleteState), new LevelCompleteState(this, _config) },
                { typeof(LoadingState), new LoadingState(this, _config) }
            };

            GameRoot.GameRoot.Current.RegisterService<IGameStateMachine>(this);
        }

        private void Start()
        {
            ChangeState<MenuState>();
        }

        private void Update()
        {
            _currentState?.Update();
        }

        // Основной generic метод
        public void ChangeState<T>() where T : GameState
        {
            ChangeState(typeof(T));
        }

        // Внутренний метод, принимающий Type
        private void ChangeState(Type stateType)
        {
            if (!_states.TryGetValue(stateType, out var newState))
            {
                Debug.LogError($"State {stateType.Name} not registered!");
                return;
            }

            if (_currentState != null)
            {
                if (_currentState == newState) return;

                _previousState = _currentState;
                _currentState.Exit();
            }

            _currentState = newState;
            _currentState.Enter();

            Debug.Log($"State changed to: {stateType.Name}");
        }

        public void ReturnToPreviousState()
        {
            if (_previousState != null)
            {
                ChangeState(_previousState.GetType()); // Теперь работает!
            }
        }

        public T GetState<T>() where T : GameState
        {
            var type = typeof(T);
            return _states.TryGetValue(type, out var state) ? state as T : null;
        }

        public Type CurrentStateType => _currentState?.GetType();
    }
}