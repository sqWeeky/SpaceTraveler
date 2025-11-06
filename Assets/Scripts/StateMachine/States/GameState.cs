using Reflex.Core;

namespace StateMachine.States
{
    public abstract class GameState : IGameState
    {
        protected Container Container { get; }

        protected GameState(Container container) =>
            Container = container;

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
            Container.Resolve<IGameStateMachine>().ChangeState<T>();
        }
    }
}