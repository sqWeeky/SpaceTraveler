using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using Reflex.Extensions;
using Reflex.Injectors;
using SpaceTraveler.Scripts.Configs;
using SpaceTraveler.Scripts.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceTraveler.Scripts.Managers
{
    public class LevelManager : BaseManager<LevelManager>
    {
        [Inject] private LevelsConfig _levelConfig;
        [Inject] private UIManager _uiManager;
        
        private string _currentLevelId;
        private string _nextLevelId;

        private void Start()
        {
            GameObjectInjector.InjectSingle(gameObject, SceneManager.GetActiveScene().GetSceneContainer());
        }

        public async Task Load(string levelId)
        {
            _nextLevelId = levelId;
            //SceneManager.LoadSceneAsync(GetSceneToLoad()); //TODO: сделать через LoadingState
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(GetSceneToLoad());
                
            if (loadOperation != null)
            {
                _uiManager.CloseAllWindows();
                _uiManager.OpenWindow<LoadingWindow>();
                
                loadOperation.allowSceneActivation = false;
                await UniTask.Delay(1200);
                loadOperation.allowSceneActivation = true;
                _uiManager.CloseWindow<LoadingWindow>();
                await UniTask.WaitUntil(() => loadOperation.isDone);

                Debug.Log($"LevelManager: Scene {GetSceneToLoad()} loaded successfully");
            }
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
                    return levelData.SceneName.name;
                }
            }

            return Constants.Levels.MainMenuID;
        }

        private LevelsConfig.LevelData GetLevelData(string levelId)
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