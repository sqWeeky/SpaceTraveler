using StateMachine.States;

namespace Windows
{
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
            UIManager.OpenWindow<LeaderboardWindow>();
        }

        public void OnExit() => GameStateMachine.ChangeState<ExitState>();
    }
}