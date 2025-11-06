using Configs;
using Game;
using Managers;
using Players;
using UnityEngine;

namespace StateMachine.States
{
    public class PausedState : GameState
    {
        public PausedState(
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
            Time.timeScale = 0f;
            UIManager.OpenWindow<PauseWindow>();
            AudioManager.PauseGameplayMusic();
            Config.TriggerGamePause();

            Debug.Log("Entered Paused State");
        }

        public override void Update()
        {
            if (InputManager.WasPausePressed || InputManager.WasResumePressed)
            {
                ChangeState<PlayingState>();
            }

            if (InputManager.WasMenuRequested)
            {
                ChangeState<MenuState>();
            }
        }

        public override void Exit()
        {
            Time.timeScale = 1f;
            UIManager.CloseWindow<PauseWindow>();
            AudioManager.ResumeGameplayMusic();
            Config.TriggerGameResume();

            Debug.Log("Exited Paused State");
        }
    }
}