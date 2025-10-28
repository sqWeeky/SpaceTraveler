using System;
using Managers;
using Players;

namespace Configs
{
    [Serializable]
    public class GameConfig
    {
        public Player Player;
        public UIManager UIManager;
        public LevelManager LevelManager;
        public InputManager InputManager;
        public AudioManager AudioManager;

        public event Action OnGameStarted;
        public event Action OnGamePaused;
        public event Action OnGameResumed;
        public event Action OnGameEnded;

        public void TriggerGameStart() => OnGameStarted?.Invoke();
        public void TriggerGamePause() => OnGamePaused?.Invoke();
        public void TriggerGameResume() => OnGameResumed?.Invoke();
        public void TriggerGameEnd() => OnGameEnded?.Invoke();
    }
}