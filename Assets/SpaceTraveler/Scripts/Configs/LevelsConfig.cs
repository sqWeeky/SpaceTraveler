using System;
using UnityEngine;

namespace SpaceTraveler.Scripts.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/Level Config")]
    public class LevelsConfig : ScriptableObject
    {
        [Serializable]
        public class LevelData
        {
            public string LevelId;
            public string SceneName;
            public bool IsLocked;
            public int RequiredStars;
        }

        public LevelData[] Levels;
    }
}