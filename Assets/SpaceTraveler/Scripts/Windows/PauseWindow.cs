using SpaceTraveler.Scripts.StateMachine.States;
using UnityEngine;

namespace SpaceTraveler.Scripts.Windows
{
    public class PauseWindow : BaseWindow
    {
        public void Continue()
        {
            GameStateMachine.ChangeState<PlayingState>();
        }

        public void OpenSettingWindow()
        {
            UIManager.OpenWindow<SettingWindow>();
        }

        public void OpenMainMenu()
        {
            GameStateMachine.ExitState<PausedState>();
            _ = LevelManager.Load(Constants.Levels.MainMenuID);
        }
    }
}