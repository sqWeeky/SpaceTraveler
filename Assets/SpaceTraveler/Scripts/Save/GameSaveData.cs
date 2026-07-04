using System;
using System.Collections.Generic;
using SpaceTraveler.Scripts.Configs;
using SpaceTraveler.Scripts.Configs.Skins;

namespace SpaceTraveler.Scripts.Save
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
        public DataShipConfig CurrentShip;
        public List<DataShipConfig> UnlockedPlayerShips;
    }

    [Serializable]
    public class LevelsData
    {
        public List<LevelsConfig.LevelData> UnlockedLevels;
    }
}