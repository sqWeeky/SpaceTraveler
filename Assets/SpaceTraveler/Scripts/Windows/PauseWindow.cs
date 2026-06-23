using SpaceTraveler.Scripts.StateMachine.States;

namespace SpaceTraveler.Scripts.Windows
{
    public class PauseWindow : BaseWindow
    {
        public void OnClose()
        {
            UIManager.CloseWindow<PauseWindow>();
            GameStateMachine.ChangeState<PlayingState>();
        }

        public void OnOpenSettingWindow()
        {
            //UIManager.CloseWindow<PauseWindow>();
            UIManager.OpenWindow<SettingWindow>();
        }

        public void OnOpenMainMenu()
        {
            LevelManager.LoadMainMenu();
            GameStateMachine.ChangeState<LoadingState>();
        }
    }
}