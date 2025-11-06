using System;
using System.Collections.Generic;
using Configs;
using Managers;
using Reflex.Core;
using StateMachine.States;
using UnityEngine;

namespace StateMachine
{
    public class GameStateMachine : MonoBehaviour, IGameStateMachine
    {
        private Dictionary<Type, GameState> _states;
        private GameState _currentState;
        private GameState _previousState;

        private Container _container;

        public void Initialize(GameConfig config, Container container)
        {
            _container = container;
            InitializeStates();
            ChangeState<MenuState>();
        }

        private void InitializeStates()
        {
            _states = new Dictionary<Type, GameState>();

            // Создаем все состояния через DI контейнер
            CreateState<MenuState>();
            CreateState<PlayingState>();
            CreateState<PausedState>();
            CreateState<GameOverState>();
            CreateState<LevelCompleteState>();
            CreateState<LoadingState>();
            CreateState<ExitState>();

            Debug.Log($"GameStateMachine: {_states.Count} states initialized with DI");
        }
        
        private void Start()
        {
            ChangeState<MenuState>();
        }
       
        private void Update()
        {
            _currentState?.Update();
        }

        public void ChangeState<T>() where T : GameState
        {
            ChangeState(typeof(T));
        }
        
        public void ReturnToPreviousState()
        {
            if (_previousState != null)
            {
                ChangeState(_previousState.GetType());
            }
        }

        public T GetState<T>() where T : GameState
        {
            var type = typeof(T);
            return _states.TryGetValue(type, out var state) ? state as T : null;
        }

        public Type CurrentStateType => _currentState?.GetType();
        
        private void CreateState<T>() where T : GameState
        {
            var stateType = typeof(T);

            try
            {
                var state = _container.Resolve<T>();
                _states[stateType] = state;
                Debug.Log($"State created: {stateType.Name}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to create state {stateType.Name}: {ex.Message}");
            }
        }
        
        private void ChangeState(Type stateType)
        {
            if (!_states.TryGetValue(stateType, out var newState))
            {
                Debug.LogError($"State {stateType.Name} not found!");
                return;
            }

            if (_currentState != null)
            {
                if (_currentState == newState) return;

                _previousState = _currentState;
                _currentState.Exit();
            }

            _currentState = newState;
            _container.Resolve<UIManager>().CloseAllWindows();
            _currentState.Enter();
            
            Debug.Log($"State changed to: {stateType.Name}");
        }

        private void OnDestroy()
        {
            // Очищаем состояния при уничтожении
            _currentState?.Exit();
            _states?.Clear();
        }

        // private Dictionary<Type, GameState> _states;
        // private GameState _currentState;
        // private GameState _previousState;
        //
        // private Container _container;
        // public Type CurrentStateType => _currentState?.GetType();
        //
        // public void Initialize(GameConfig config, Container container)
        // {
        //     _container = container;
        //     _states = new Dictionary<Type, GameState>();
        // }
        //
        // private void Start()
        // {
        //     ChangeState<PlayingState>();
        // }
        //
        // private void Update()
        // {
        //     _currentState?.Update();
        // }
        //
        // public void ChangeState<T>() where T : GameState
        // {
        //     var newState = GetOrCreateState<T>();
        //
        //     if (_currentState != null)
        //     {
        //         if (_currentState == newState) return;
        //
        //         _previousState = _currentState;
        //         _currentState.Exit();
        //     }
        //
        //     _currentState = newState;
        //     _currentState.Enter();
        //
        //     Debug.Log($"State changed to: {typeof(T).Name}");
        // }
        //
        // public void ReturnToPreviousState()
        // {
        //     if (_previousState != null)
        //     {
        //         ChangeState(_previousState.GetType());
        //     }
        // }
        //
        // public T GetState<T>() where T : GameState
        // {
        //     var type = typeof(T);
        //     return _states.TryGetValue(type, out var state) ? state as T : null;
        // }
        //
        // private T GetOrCreateState<T>() where T : GameState
        // {
        //     var stateType = typeof(T);
        //
        //     if (!_states.TryGetValue(stateType, out var state))
        //     {
        //         // Создаем состояние лениво при первом использовании
        //         state = _container.Resolve<T>();
        //         _states[stateType] = state;
        //         Debug.Log($"State created lazily: {stateType.Name}");
        //     }
        //
        //     return state as T;
        // }
        //
        // private void ChangeState(Type stateType)
        // {
        //     if (!_states.TryGetValue(stateType, out var newState))
        //     {
        //         Debug.LogError($"State {stateType.Name} not registered!");
        //         return;
        //     }
        //
        //     if (_currentState != null)
        //     {
        //         if (_currentState == newState) return;
        //
        //         _previousState = _currentState;
        //         _currentState.Exit();
        //     }
        //
        //     _currentState = newState;
        //     _currentState.Enter();
        //
        //     Debug.Log($"State changed to: {stateType.Name}");
        // }
    }
}