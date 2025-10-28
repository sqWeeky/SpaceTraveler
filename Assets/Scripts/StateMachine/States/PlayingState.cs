using Configs;
using UnityEngine;

namespace StateMachine.States
{
    public class PlayingState : GameState
    {
        private float _gameTime;
    
        public PlayingState(GameStateMachine stateMachine, GameConfig config) 
            : base(stateMachine, config) { }

        public override void Enter()
        {
            Config.UIManager.ShowGameHUD();
            Config.InputManager.EnableGameplayInput();
            //_context.AudioManager.PlayMusic(AudioType.GameplayMusic);
        
            // Подписываемся на события
            Config.Player.OnPlayerDied += OnPlayerDied;
            Config.Player.OnLevelComplete += OnLevelComplete;
        
            _gameTime = 0f;
            Config.TriggerGameStart();
        
            Debug.Log("Entered Playing State");
        }

        public override void Update()
        {
            _gameTime += Time.deltaTime;
            Config.UIManager.UpdateGameTimer(_gameTime);
        
            // Проверка паузы
            if (Config.InputManager.WasPausePressed)
            {
                ChangeState<PausedState>();
            }
        }

        public override void Exit()
        {
            Config.InputManager.DisableGameplayInput();
            Config.Player.OnPlayerDied -= OnPlayerDied;
            Config.Player.OnLevelComplete -= OnLevelComplete;
        
            Debug.Log("Exited Playing State");
        }
    
        private void OnPlayerDied()
        {
            ChangeState<GameOverState>();
        }
    
        private void OnLevelComplete()
        {
            ChangeState<LevelCompleteState>();
        }
    }
}