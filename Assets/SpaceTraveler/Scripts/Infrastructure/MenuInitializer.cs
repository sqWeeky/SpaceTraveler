using Reflex.Attributes;
using SpaceTraveler.Scripts.StateMachine;
using SpaceTraveler.Scripts.StateMachine.States;
using UnityEngine;

namespace SpaceTraveler.Scripts.Infrastructure
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