using System;
using System.Collections.Generic;
using Configs.Skins;
using Infrastructure;
using Skins;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = nameof(GameConfig))]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private PlayerConfig _playerData;
        [SerializeField] private LevelsConfig _levels;
        
        [Header("List ships")]
        [SerializeField] private SerializableDictionary<DataShipConfig, List<Skin>> _shipConfigs;
        
        public PlayerConfig PlayerData => _playerData;
        public LevelsConfig Levels => _levels;
        public SerializableDictionary<DataShipConfig, List<Skin>> ShipConfigs => _shipConfigs;
        
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