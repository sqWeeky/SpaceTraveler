using System;
using System.Collections.Generic;
using Configs;
using Configs.Skins;
using UnityEngine;

namespace Save
{
    [Serializable]
    public class GameSaveData
    {
        public PlayerData PlayerData;
        public LevelsData LevelsData;
    }

    [Serializable]
    public class PlayerData
    {
        public string PlayerName;
        public int PlayerScore;
        public int PlayerStarScore;
        public List<DataShipConfig> UnlockedPlayerShips;
    }

    [Serializable]
    public class LevelsData
    {
        public List<LevelsConfig.LevelData> UnlockedLevels;
    }
}