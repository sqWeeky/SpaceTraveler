using Reflex.Attributes;
using SpaceTraveler.Scripts.Managers;
using SpaceTraveler.Scripts.StateMachine;
using SpaceTraveler.Scripts.StateMachine.States;
using SpaceTraveler.Scripts.Windows;
using UnityEngine;

namespace SpaceTraveler.Scripts.Infrastructure
{
    public class MenuInitializer : MonoBehaviour
    {
        [Inject] private GameStateMachine _stateMachine;
        [Inject] private UIManager _uiManager;

        private void Start()
        {
            _stateMachine.ChangeState<MenuState>();
        }
    }
}