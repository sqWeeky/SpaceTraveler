using Managers;
using Reflex.Core;
using StateMachine;
using StateMachine.States;
using UnityEngine.SceneManagement;

public class LevelsMenuWindow : BaseWindow
{
    public void OnStartGame()
    {
        SceneManager.LoadScene("TestLevel");
        Container.ProjectContainer.Resolve<GameStateMachine>().ChangeState<PlayingState>();
    }

    public void OnBack() => UIManager.CloseWindow<LevelsMenuWindow>();
}
