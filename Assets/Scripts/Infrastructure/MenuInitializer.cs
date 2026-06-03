using Configs;
using Managers;
using Reflex.Attributes;
using StateMachine;
using StateMachine.States;
using UnityEngine;

namespace Infrastructure
{
    public class MenuInitializer : MonoBehaviour
    {
        // [Inject] private UIManager _uiManager;
        // [Inject] private LevelManager _levelManager;
        // [Inject] private UIManagerConfig _uiManagerConfig;
        // [Inject] private LevelsConfig _levelsConfig;
        [Inject] private GameStateMachine _stateMachine;

        private void Start()
        {
           // _levelManager.Init(_levelsConfig);

            _stateMachine.ChangeState<MenuState>();
        }
    }
}