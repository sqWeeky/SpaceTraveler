using System;
using UnityEditor;

namespace SpaceTraveler.Scripts.Data
{
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