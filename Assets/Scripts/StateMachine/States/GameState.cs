using Managers;

namespace StateMachine.States
{
    public abstract class GameState : IGameState
    {
        protected GameStateMachine GameStateMachine { get; private set; }
        protected UIManager UIManager { get; private set; }
        protected AudioManager AudioManager { get; private set; }
        protected InputManager InputManager { get; private set; }
        protected LevelManager LevelManager { get; private set; }

        protected GameState(
            GameStateMachine gameStateMachine,
            UIManager uiManager,
            AudioManager audioManager,
            InputManager inputManager,
            LevelManager levelManager)
        {
            GameStateMachine = gameStateMachine;
            UIManager = uiManager;
            AudioManager = audioManager;
            InputManager = inputManager;
            LevelManager = levelManager;
        }

        public virtual void Enter()
        {
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