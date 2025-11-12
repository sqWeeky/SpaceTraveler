using Configs;
using Game;
using Managers;
using Players;
using Reflex.Core;
using UnityEngine;

namespace StateMachine.States
{
    public class PlayingState : GameState
    {
        private float _gameTime;
        private PlayingWindow _playingWindow;

        // public PlayingState(Container container) : base(container)
        // {
        // }

        public override void Enter()
        {
            //GameRoot.Instance.GetManager<UIManager>().OpenWindow<PlayingWindow>();
            //Container.Resolve<UIManager>().CloseAllWindows();
            // _playingWindow = (PlayingWindow)Container.Resolve<UIManager>().OpenWindow<PlayingWindow>();
            // Container.Resolve<InputManager>().EnableGameplayInput();
            //AudioManager.PlayMusic(AudioType.GameplayMusic);


            // Player.OnPlayerDied += OnPlayerDied;
            // Player.OnLevelComplete += OnLevelComplete;

            _gameTime = 0f;
            //Container.Resolve<GameConfig>().TriggerGameStart();

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
            // Container.Resolve<UIManager>().CloseWindow<PlayingWindow>();
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