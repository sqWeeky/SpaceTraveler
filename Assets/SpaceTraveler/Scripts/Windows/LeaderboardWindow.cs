namespace SpaceTraveler.Scripts.Windows
{
    public class LeaderboardWindow : BaseWindow
    {
        public override void CloseWindow()
        {
            UIManager.OpenWindow<MainMenuWindow>();
            UIManager.CloseWindow<LeaderboardWindow>();
        }
    }
}
