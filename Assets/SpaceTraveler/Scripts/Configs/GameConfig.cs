using System;
using System.Collections.Generic;
using SpaceTraveler.Scripts.Configs.Skins;
using SpaceTraveler.Scripts.Infrastructure;
using SpaceTraveler.Scripts.Skins;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpaceTraveler.Scripts.Configs
{
    [CreateAssetMenu(menuName = nameof(GameConfig))]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private LevelsConfig _levels;
        
        [Header("List ships")]
        [SerializeField] private SerializableDictionary<DataShipConfig, List<Skin>> _shipConfigs;
        
        public PlayerConfig PlayerConfig => _playerConfig;
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