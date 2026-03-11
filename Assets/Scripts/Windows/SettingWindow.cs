using StateMachine.States;

namespace Windows
{
    public class SettingWindow : BaseWindow
    {
        public void OnCloseSettingWindow()
        {
            // if (GameStateMachine.CurrentStateType == typeof(PausedState))
            //     UIManager.OpenWindow<PauseWindow>();

            UIManager.CloseWindow<SettingWindow>();
        }
    }
}