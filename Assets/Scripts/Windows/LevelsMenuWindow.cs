using Managers;
using StateMachine.States;
using UnityEngine.SceneManagement;

public class LevelsMenuWindow : BaseWindow
{
    public void OnStartGame()
    {
        SceneManager.LoadScene("TestLevel");
        GameStateMachine.ChangeState<PlayingState>();
    }

    public void OnBack() => UIManager.CloseWindow<LevelsMenuWindow>();
}
