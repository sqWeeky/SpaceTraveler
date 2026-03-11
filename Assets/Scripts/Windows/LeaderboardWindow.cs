using Windows;

public class LeaderboardWindow : BaseWindow
{
    public void OnBack() => UIManager.CloseWindow<GameModeWindow>();
}
