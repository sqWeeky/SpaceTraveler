using Managers;
using StateMachine.States;

public class MainMenuWindow : BaseWindow
{
    public void OnStart()
    {
        UIManager.OpenWindow<GameModeWindow>();
    }

    public void OnOpenSettingWindow()
    {
        UIManager.OpenWindow<SettingWindow>();
    }

    public void OnOpenLeaderboard()
    {
    }

    public void OnExit() => GameStateMachine.ChangeState<ExitState>();
}