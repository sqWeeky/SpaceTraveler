using SpaceTraveler.Scripts.StateMachine.States;

namespace SpaceTraveler.Scripts.Windows
{
    public class MainMenuWindow : BaseWindow
    {
        public override void OnStart()
        {
            base.OnStart();
            UIManager.OpenWindow<GameModeWindow>();
        }

        public void OnOpenShop()
        {
            UIManager.OpenWindow<ShopWindow>();
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