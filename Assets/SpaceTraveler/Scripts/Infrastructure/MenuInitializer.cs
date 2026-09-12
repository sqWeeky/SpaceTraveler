using Reflex.Attributes;
using SpaceTraveler.Scripts.StateMachine;
using SpaceTraveler.Scripts.StateMachine.States;
using UnityEngine;

namespace SpaceTraveler.Scripts.Infrastructure
{
    public class MenuInitializer : MonoBehaviour
    {
        [Inject] private GameStateMachine _stateMachine;

        private void Start()
        {
            _stateMachine.ChangeState<PlayingState>();
        }
    }
}