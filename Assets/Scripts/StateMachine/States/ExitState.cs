using Configs;
using Managers;
using Players;
using Reflex.Core;
using UnityEditor;

namespace StateMachine.States
{
    public class ExitState : GameState
    {
        public ExitState(Container container) : base(container)
        {
        }

        public override void Enter()
        {
            EditorApplication.ExitPlaymode();
        }

        public override void Update()
        {
        }

        public override void Exit()
        {
        }
    }
}