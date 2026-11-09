namespace SpaceTraveler.Scripts.Windows
{
    public class SettingWindow : BaseWindow
    {
        // public void OnCloseSettingWindow()
        // {
        //     // if (GameStateMachine.CurrentStateType == typeof(PausedState))
        //     //     UIManager.OpenWindow<PauseWindow>();
        //
        //     
        // }

        public override void CloseWindow()
        {
            UIManager.OpenWindow<MainMenuWindow>();
            UIManager.CloseWindow<SettingWindow>();
        }
    }
}