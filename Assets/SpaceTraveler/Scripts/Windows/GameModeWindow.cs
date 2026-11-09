using SpaceTraveler.Scripts.Managers;

namespace SpaceTraveler.Scripts.Windows
{
    public class GameModeWindow : BaseWindow
    {
        public void OnOpenLevelsWindow()
        {
            UIManager.OpenWindow<LevelsMenuWindow>();
            UIManager.CloseWindow<GameModeWindow>();
        }

        public void OnOpenEndlessGame()
        {
        
        }
    
        public override void CloseWindow()
        {
            UIManager.OpenWindow<MainMenuWindow>();
            UIManager.CloseWindow<GameModeWindow>();
        }
    }
}
