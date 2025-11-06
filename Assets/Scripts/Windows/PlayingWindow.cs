using Managers;
using StateMachine.States;

public class PlayingWindow : BaseWindow
{
    public void OnOpen() => GameStateMachine.ChangeState<PausedState>();

    public void UpdateGameTimer(float gameTime)
    {
        
    }
}