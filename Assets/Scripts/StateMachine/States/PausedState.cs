using Windows;
using Managers;
using UnityEngine;

namespace StateMachine.States
{
    public class PausedState : GameState
    {
        public PausedState(
            GameStateMachine gameStateMachine, UIManager uiManager, AudioManager audioManager,
            InputManager inputManager, LevelManager levelManager) : base(gameStateMachine, uiManager, audioManager,
            inputManager, levelManager)
        {
        }

        public override void Enter()
        {
            Time.timeScale = 0f;
            UIManager.OpenWindow<PauseWindow>();
            AudioManager.PauseGameplayMusic();
            // Container.Resolve<GameConfig>().TriggerGamePause();

            Debug.Log("Entered Paused State");
        }

        public override void Update()
        {
            // if (Container.Resolve<InputManager>().WasPausePressed || Container.Resolve<InputManager>().WasResumePressed)
            // {
            //     ChangeState<PlayingState>();
            // }
            //
            // if (Container.Resolve<InputManager>().WasMenuRequested)
            // {
            //     ChangeState<MenuState>();
            // }
        }

        public override void Exit()
        {
            Time.timeScale = 1f;
            UIManager.CloseWindow<PauseWindow>();
            AudioManager.ResumeGameplayMusic();
            // Container.Resolve<GameConfig>().TriggerGameResume();

            Debug.Log("Exited Paused State");
        }
    }
}