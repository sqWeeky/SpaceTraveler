using Managers;
using UnityEditor;

namespace StateMachine.States
{
    public class ExitState : GameState
    {
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