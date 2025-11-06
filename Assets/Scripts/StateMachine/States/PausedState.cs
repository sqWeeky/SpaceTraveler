using Configs;
using UnityEngine;

namespace StateMachine.States
{
    public class PausedState : GameState
    {
        public PausedState(GameStateMachine stateMachine, GameConfig config) 
            : base(stateMachine, config) { }

        public override void Enter()
        {
            Time.timeScale = 0f;
            Config.UIManager.ShowPauseScreen();
            Config.AudioManager.PauseGameplayMusic();
            Config.TriggerGamePause();
        
            Debug.Log("Entered Paused State");
        }

        public override void Update()
        {
            if (Config.InputManager.WasPausePressed || Config.InputManager.WasResumePressed)
            {
                ChangeState<PlayingState>();
            }
        
            if (Config.InputManager.WasMenuRequested)
            {
                ChangeState<MenuState>();
            }
        }

        public override void Exit()
        {
            Time.timeScale = 1f;
            Config.UIManager.HidePauseScreen();
            Config.AudioManager.ResumeGameplayMusic();
            Config.TriggerGameResume();
        
            Debug.Log("Exited Paused State");
        }
    }
}