using StateMachine.States;

namespace Windows
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