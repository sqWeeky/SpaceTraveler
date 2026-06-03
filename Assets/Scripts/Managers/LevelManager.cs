using Configs;
using Reflex.Attributes;
using Reflex.Extensions;
using Reflex.Injectors;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class LevelManager : BaseManager<LevelManager>
    {
        [Inject] private LevelsConfig _levelConfig;
        
        private string _currentLevelId;
        private string _nextLevelId;

        private void Start()
        {
            GameObjectInjector.InjectSingle(gameObject, SceneManager.GetActiveScene().GetSceneContainer());
        }

        // public void Init(LevelsConfig config)
        // {
        //     _levelConfig = config;
        // }

        public void LoadLevel(string levelId)
        {
            _nextLevelId = levelId;
        }
        
        public void LoadMainMenu()
        {
            _nextLevelId = null;
            _currentLevelId = null;
            Debug.Log("LevelManager: Loading main menu");
        }

        public string GetSceneToLoad()
        {
            if (!string.IsNullOrEmpty(_nextLevelId))
            {
                var levelData = GetLevelData(_nextLevelId);
                
                if (levelData != null)
                {
                    _currentLevelId = _nextLevelId;
                    _nextLevelId = null;
                    return levelData.SceneName;
                }
            }

            // Если нет следующего уровня, возвращаем главное меню
            return "MainMenu";
        }

        public LevelsConfig.LevelData GetLevelData(string levelId)
        {
            foreach (var level in _levelConfig.Levels)
            {
                if (level.LevelId == levelId)
                    return level;
            }

            return null;
        }

        public LevelsConfig.LevelData GetCurrentLevelData()
        {
            return GetLevelData(_currentLevelId);
        }

        public string[] GetAvailableLevels()
        {
            return System.Array.ConvertAll(_levelConfig.Levels, level => level.LevelId);
        }
    }
}