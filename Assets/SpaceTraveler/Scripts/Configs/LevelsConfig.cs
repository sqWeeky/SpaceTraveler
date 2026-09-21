using System;
using UnityEditor;
using UnityEngine;

namespace SpaceTraveler.Scripts.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/Level Config")]
    public class LevelsConfig : ScriptableObject
    {
        public LevelData[] Levels;
        
        [Serializable]
        public class LevelData
        {
            public string LevelId;
            public SceneAsset SceneName;
            public bool IsLocked;
            public int RequiredStars;
            public float BestTime;
        }
    }
}