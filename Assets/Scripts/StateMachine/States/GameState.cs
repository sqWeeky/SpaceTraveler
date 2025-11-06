using Configs;
using Managers;
using Players;

namespace StateMachine.States
{
    public abstract class GameState : IGameState
    {
        protected GameStateMachine StateMachine { get; }
        protected GameConfig Config { get; }
        
        protected UIManager UIManager { get; }
        protected AudioManager AudioManager { get; }
        protected InputManager InputManager { get; }
        protected LevelManager LevelManager { get; }
        protected Player Player { get; }
        
        protected GameState(
            GameStateMachine stateMachine,
            GameConfig config,
            UIManager uiManager,
            AudioManager audioManager, 
            InputManager inputManager,
            LevelManager levelManager,
            Player player)
        {
            StateMachine = stateMachine;
            Config = config;
            UIManager = uiManager;
            AudioManager = audioManager;
            InputManager = inputManager;
            LevelManager = levelManager;
            Player = player;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    
        protected void ChangeState<T>() where T : GameState
        {
            StateMachine.ChangeState<T>();
        }
    }
}