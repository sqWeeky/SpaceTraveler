using Configs;
using UnityEngine;

namespace StateMachine.States
{
    public class GameOverState : GameState
    {
        private float _timer;
    
        public GameOverState(GameStateMachine stateMachine, GameConfig config) 
            : base(stateMachine, config) { }

        public override void Enter()
        {
            Config.UIManager.ShowGameOverScreen();
            //_context.AudioManager.PlaySFX(AudioType.GameOver);
            Config.TriggerGameEnd();
        
            _timer = 0f;
            Debug.Log("Entered GameOver State");
        }

        public override void Update()
        {
            _timer += Time.unscaledDeltaTime;
        
            // Автоматический возврат в меню через 3 секунды
            if (_timer >= 3f && Config.InputManager.AnyInput)
            {
                ChangeState<MenuState>();
            }
        }

        public override void Exit()
        {
            Config.UIManager.HideGameOverScreen();
            Debug.Log("Exited GameOver State");
        }
    }
}