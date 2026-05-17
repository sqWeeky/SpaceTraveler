using Managers;
using Reflex.Attributes;
using UnityEngine;

namespace StateMachine.States
{
    public abstract class GameState : IGameState
    {
        [Inject]protected GameStateMachine GameStateMachine { get; private set; }
        [Inject]protected UIManager UIManager { get; private set; }
        [Inject] protected AudioManager AudioManager { get; private set; }
        [Inject] protected InputManager InputManager { get; private set; }
        [Inject] protected LevelManager LevelManager { get; private set; }

        public virtual void Enter()
        {
            Debug.Log("Entered Game State");
            Debug.Log(GameStateMachine != null);
            Debug.Log(UIManager != null);
            Debug.Log(AudioManager != null);
            Debug.Log(InputManager != null);
            Debug.Log(LevelManager != null);
        }

        public virtual void Update()
        {
        }

        public virtual void Exit()
        {
        }

        protected void ChangeState<T>() where T : GameState
        {
            GameStateMachine.ChangeState<T>();
        }
    }
}