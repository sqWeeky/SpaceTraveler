using SpaceTraveler.Scripts.StateMachine.States;

namespace SpaceTraveler.Scripts.Windows
{
    public class PlayingWindow : BaseWindow
    {
        public void OnOpen()
        {
            GameStateMachine.ChangeState<PausedState>();
        }

        public void UpdateGameTimer(float gameTime)
        {
        
        }
    }
}