using Managers;
using StateMachine.States;
using UnityEngine.SceneManagement;

public class PauseWindow : BaseWindow
{
    public void OnClose() => GameStateMachine.ChangeState<PlayingState>();

    public void OnOpenSettingWindow()
    {
        //UIManager.CloseWindow<PauseWindow>();
        UIManager.OpenWindow<SettingWindow>();
    }

    public void OnOpenMainMenu()
    {
        GameStateMachine.ChangeState<MenuState>();
        SceneManager.LoadScene("MainMenu");
    }
}