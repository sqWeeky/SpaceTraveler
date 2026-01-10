using Managers;
using UnityEngine;

namespace StateMachine.States
{
    public class GameOverState : GameState
    {
        private float _timer;

        public GameOverState(GameStateMachine gameStateMachine, UIManager uiManager, AudioManager audioManager,
            InputManager inputManager, LevelManager levelManager) : base(gameStateMachine, uiManager, audioManager,
            inputManager, levelManager)
        {
        }

        public override void Enter()
        {
            //GameRoot.Instance.GetManager<UIManager>().ShowGameOverScreen();
            //_context.AudioManager.PlaySFX(AudioType.GameOver);

            //Container.Resolve<GameConfig>().TriggerGameEnd();

            _timer = 0f;
            Debug.Log("Entered GameOver State");
        }

        public override void Update()
        {
            _timer += Time.unscaledDeltaTime;

            // Автоматический возврат в меню через 3 секунды
            // if (_timer >= 3f && Container.Resolve<InputManager>().AnyInput)
            // {
            //     ChangeState<MenuState>();
            // }
        }

        public override void Exit()
        {
            //GameRoot.Instance.GetManager<UIManager>().HideGameOverScreen();
            Debug.Log("Exited GameOver State");
        }
    }
}