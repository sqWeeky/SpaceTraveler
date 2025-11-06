using Configs;
using Managers;
using Players;
using UnityEditor;

namespace StateMachine.States
{
    public class ExitState : GameState
    {
        public ExitState(
            GameStateMachine stateMachine, 
            GameConfig config, 
            UIManager uiManager,
            AudioManager audioManager, 
            InputManager inputManager, 
            LevelManager levelManager, 
            Player player) : 
            base(stateMachine, config, uiManager, audioManager, inputManager, levelManager, player)
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