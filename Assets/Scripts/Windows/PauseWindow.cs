using Managers;
using StateMachine.States;

public class PauseWindow : BaseWindow
{
    public void OnClose()
    {
        GameStateMachine.ChangeState<PlayingState>();
    }

    public void OnOpenSettingWindow()
    {
        UIManager.CloseWindow<PauseWindow>();
        UIManager.OpenWindow<SettingWindow>();
    }

    public void OnOpenMainMenu()
    {
        LevelManager.LoadMainMenu();
        GameStateMachine.ChangeState<LoadingState>();
    }
}