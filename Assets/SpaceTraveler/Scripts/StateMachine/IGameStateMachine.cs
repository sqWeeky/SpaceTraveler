using System;
using SpaceTraveler.Scripts.StateMachine.States;

namespace SpaceTraveler.Scripts.StateMachine
{
    public interface IGameStateMachine
    {
        void ChangeState<T>() where T : GameState;
        void ReturnToPreviousState();
        Type CurrentStateType { get; }
    }
}