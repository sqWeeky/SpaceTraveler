using System;
using System.Collections.Generic;
using Configs.Skins;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = nameof(GameConfig))]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private PlayerConfig _playerData;
        [SerializeField] private LevelsConfig _levels;
        [SerializeField] private List<DataShipConfig> _dataShips;
        
        public PlayerConfig PlayerData => _playerData;
        public LevelsConfig Levels => _levels;
        public List<DataShipConfig> DataShips => _dataShips;
        
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