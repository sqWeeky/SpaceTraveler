using Configs;
using Game;
using Managers;
using Players;
using Reflex.Core;
using UnityEngine;

namespace StateMachine.States
{
    public class PausedState : GameState
    {
        public PausedState(Container container) : base(container)
        {
        }

        public override void Enter()
        {
            Time.timeScale = 0f;
            Container.Resolve<UIManager>().OpenWindow<PauseWindow>();
            Container.Resolve<AudioManager>().PauseGameplayMusic();
            Container.Resolve<GameConfig>().TriggerGamePause();

            Debug.Log("Entered Paused State");
        }

        public override void Update()
        {
            if (Container.Resolve<InputManager>().WasPausePressed || Container.Resolve<InputManager>().WasResumePressed)
            {
                ChangeState<PlayingState>();
            }

            if (Container.Resolve<InputManager>().WasMenuRequested)
            {
                ChangeState<MenuState>();
            }
        }

        public override void Exit()
        {
            Time.timeScale = 1f;
            Container.Resolve<UIManager>().CloseWindow<PauseWindow>();
            Container.Resolve<AudioManager>().ResumeGameplayMusic();
            Container.Resolve<GameConfig>().TriggerGameResume();

            Debug.Log("Exited Paused State");
        }
    }
}