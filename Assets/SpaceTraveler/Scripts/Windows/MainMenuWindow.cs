using SpaceTraveler.Scripts.StateMachine.States;

namespace SpaceTraveler.Scripts.Windows
{
    public class MainMenuWindow : BaseWindow
    {
        public override void OnStart()
        {
            base.OnStart();
            UIManager.OpenWindow<GameModeWindow>();
            UIManager.CloseWindow<MainMenuWindow>();
        }

        public void OnOpenShop()
        {
            UIManager.OpenWindow<ShopWindow>();
            UIManager.CloseWindow<MainMenuWindow>();
        }

        public void OnOpenSettingWindow()
        {
            UIManager.OpenWindow<SettingWindow>();
            UIManager.CloseWindow<MainMenuWindow>();
        }

        public void OnOpenLeaderboard()
        {
            UIManager.OpenWindow<LeaderboardWindow>();
            UIManager.CloseWindow<MainMenuWindow>();
        }

        public void OnExit() => GameStateMachine.ChangeState<ExitState>();
    }
}