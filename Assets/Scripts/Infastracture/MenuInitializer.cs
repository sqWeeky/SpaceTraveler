using Configs;
using Managers;
using Reflex.Attributes;
using StateMachine;
using StateMachine.States;
using UnityEngine;

namespace Infastracture
{
    public class MenuInitializer : MonoBehaviour
    {
        [Inject]
        private void Inject(
            GameStateMachine gameStateMachine, UIManager uiManager, UIManagerConfig config, LevelManager levelManager,
            LevelsConfig levelsConfig)
        {
            uiManager.Init(config);
            levelManager.Init(levelsConfig);

            gameStateMachine.ChangeState<MenuState>();
        }
    }
}