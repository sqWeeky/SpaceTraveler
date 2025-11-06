using Configs;
using Game;
using Managers;
using Players;
using UnityEngine;

namespace StateMachine.States
{
    public class GameOverState : GameState
    {
        private float _timer;

        public GameOverState(
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
            //GameRoot.Instance.GetManager<UIManager>().ShowGameOverScreen();
            //_context.AudioManager.PlaySFX(AudioType.GameOver);
            Config.TriggerGameEnd();

            _timer = 0f;
            Debug.Log("Entered GameOver State");
        }

        public override void Update()
        {
            _timer += Time.unscaledDeltaTime;

            // Автоматический возврат в меню через 3 секунды
            if (_timer >= 3f && GameRoot.Instance.GetManager<InputManager>().AnyInput)
            {
                ChangeState<MenuState>();
            }
        }

        public override void Exit()
        {
            //GameRoot.Instance.GetManager<UIManager>().HideGameOverScreen();
            Debug.Log("Exited GameOver State");
        }
    }
}