using Managers;
using UnityEditor;

namespace StateMachine.States
{
    public class ExitState : GameState
    {
        public ExitState(GameStateMachine gameStateMachine, UIManager uiManager, AudioManager audioManager,
            InputManager inputManager, LevelManager levelManager) : base(gameStateMachine, uiManager, audioManager,
            inputManager, levelManager)
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