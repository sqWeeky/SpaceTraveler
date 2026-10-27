using SpaceTraveler.Scripts.Windows;
using UnityEngine;

namespace SpaceTraveler.Scripts.StateMachine.States
{
    public class PlayingState : GameState
    {
        private float _gameTime;
        private PlayingWindow _playingWindow;

        public override void Enter()
        {
            Time.timeScale = 1f;
            Debug.Log("Entered Playing State");

            UIManager.CloseAllWindows();
            _playingWindow = UIManager.OpenWindow<PlayingWindow>();
            _playingWindow.Timer.StartTimer();
            //AudioManager.PlayMusic(AudioType.GameplayMusic);

            // Player.OnPlayerDied += OnPlayerDied;
            // Player.OnLevelComplete += OnLevelComplete;
        }

        public override void Update()
        {
            //_playingWindow.UpdateGameTimer(_gameTime);

            // Проверка паузы
            // if (GameRoot.Instance.GetManager<InputManager>().WasPausePressed)
            // {
            //     ChangeState<PausedState>();
            // }
        }

        public override void Exit()
        {
           // _playingWindow.Timer.StopTimer();
            
            UIManager.CloseWindow<PlayingWindow>();

            // Player.OnPlayerDied -= OnPlayerDied;
            // Player.OnLevelComplete -= OnLevelComplete;

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