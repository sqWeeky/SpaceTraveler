using Reflex.Core;
using StateMachine;
using StateMachine.States;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialSceneInstaller : MonoBehaviour, IInstaller
{
    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        SceneManager.LoadScene("MainMenu");
        Container.ProjectContainer.Resolve<GameStateMachine>().ChangeState<MenuState>();
    }
}