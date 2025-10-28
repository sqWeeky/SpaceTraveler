using Configs;

namespace StateMachine.States
{
    public abstract class GameState : IGameState
    {
        private GameStateMachine _stateMachine;
        protected GameConfig Config;

        protected GameState(GameStateMachine stateMachine, GameConfig config)
        {
            _stateMachine = stateMachine;
            Config = config;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    
        protected void ChangeState<T>() where T : GameState
        {
            _stateMachine.ChangeState<T>();
        }
    }
}