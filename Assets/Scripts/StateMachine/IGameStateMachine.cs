using System;
using StateMachine.States;

namespace StateMachine
{
    public interface IGameStateMachine
    {
        void ChangeState<T>() where T : GameState;
        void ReturnToPreviousState();
        Type CurrentStateType { get; }
    }
}