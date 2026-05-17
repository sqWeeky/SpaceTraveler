using Windows;
using Managers;
using Players;
using UnityEngine;

namespace StateMachine.States
{
    public class PlayingState : GameState
    {
        private float _gameTime;
        private PlayingWindow _playingWindow;

        public override void Enter()
        {
             UIManager.CloseAllWindows();
            _playingWindow = (PlayingWindow)UIManager.OpenWindow<PlayingWindow>();
            //AudioManager.PlayMusic(AudioType.GameplayMusic);

            // Player.OnPlayerDied += OnPlayerDied;
            // Player.OnLevelComplete += OnLevelComplete;

            _gameTime = 0f;
            
            Debug.Log("Entered Playing State");
        }

        public override void Update()
        {
            _gameTime += Time.deltaTime;
            //_playingWindow.UpdateGameTimer(_gameTime);

            // Проверка паузы
            // if (GameRoot.Instance.GetManager<InputManager>().WasPausePressed)
            // {
            //     ChangeState<PausedState>();
            // }
        }

        public override void Exit()
        {
            UIManager.CloseWindow<PlayingWindow>();
            // Container.Resolve<InputManager>().DisableGameplayInput();
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