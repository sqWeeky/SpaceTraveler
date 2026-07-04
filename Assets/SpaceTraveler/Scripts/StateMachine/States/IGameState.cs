namespace SpaceTraveler.Scripts.StateMachine.States
{
    public interface IGameState 
    {
        void Enter();
        void Update();
        void Exit();
    }
}