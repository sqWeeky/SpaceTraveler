using SpaceTraveler.Scripts.Data;
using UnityEngine;

namespace SpaceTraveler.Scripts.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/Level Config")]
    public class LevelsConfig : ScriptableObject
    {
        public LevelData[] Levels;
    }
}