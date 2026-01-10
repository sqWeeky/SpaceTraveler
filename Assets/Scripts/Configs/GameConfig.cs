using System;
using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = nameof(GameConfig))]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private List<LevelsConfig> _levels;
        
        public List<LevelsConfig> Levels => _levels;
        
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