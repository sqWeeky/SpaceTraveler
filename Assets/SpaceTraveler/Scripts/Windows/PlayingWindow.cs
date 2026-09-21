using SpaceTraveler.Scripts.StateMachine.States;

namespace SpaceTraveler.Scripts.Windows
{
    public class PlayingWindow : BaseWindow
    {
        public void PauseOpen()
        {
           GameStateMachine.ChangeState<PausedState>();
        }

        public void UpdateGameTimer(float gameTime)
        {
        
        }
    }
}