using Managers;

public class GameModeWindow : BaseWindow
{
    public void OnOpenLevelsWindow() => UIManager.OpenWindow<LevelsMenuWindow>();

    public void OnOpenEndlessGame()
    {
        
    }
    
    public void OnBack() => UIManager.CloseWindow<GameModeWindow>();
}
