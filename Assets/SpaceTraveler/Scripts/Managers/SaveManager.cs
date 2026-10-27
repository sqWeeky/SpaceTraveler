using Reflex.Attributes;
using Reflex.Extensions;
using Reflex.Injectors;
using SpaceTraveler.Scripts.Configs;
using SpaceTraveler.Scripts.Configs.Skins;
using SpaceTraveler.Scripts.Data;
using SpaceTraveler.Scripts.Save;
using SpaceTraveler.Scripts.Skins;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceTraveler.Scripts.Managers
{
    /// <summary>
    /// SaveManager — менеджер для сохранения и загрузки игровых данных.
    /// 
    /// Паттерн: наследуется от BaseManager<T>, регистрируется как синглтон в ProjectInstaller,
    /// получает доступ к конфигам через [Inject] и синхронизирует данные через DTO (GameSaveSnapshot).
    /// 
    /// API:
    /// - Save() — сохраняет текущие данные в JSON файл
    /// - Load() — загружает данные из файла в память и синхронизирует с менеджерами
    /// - Clear() — удаляет файл сохранения (сброс)
    /// - GetSnapshot() — возвращает DTO для отправки в SDK или UI
    /// - AddCompletedLevel() — добавляет пройденный уровень
    /// - AddSkinPurchase() — записывает покупку скина с временем
    /// </summary>
    public class SaveManager : BaseManager<SaveManager>
    {
        [Inject] private GameConfig _gameConfig;

        private GameSaveData _gameSaveData;

        public GameSaveData GameSaveData => _gameSaveData;

        private void Start()
        {
            GameObjectInjector.InjectSingle(gameObject, SceneManager.GetActiveScene().GetSceneContainer());

            _gameSaveData = Load();

            Debug.Log($"_gameSaveData - {_gameSaveData != null}");

            if (_gameSaveData == null)
            {
                _gameSaveData = new GameSaveData
                {
                    PlayerData = new PlayerData
                    {
                        PlayerName = _gameConfig.PlayerConfig.PlayerName,
                        PlayerScore = _gameConfig.PlayerConfig.CrystalsScore,
                        PlayerStarScore = _gameConfig.PlayerConfig.StarScore,
                        CurrentShip = _gameConfig.PlayerConfig.DefaultDataShip,
                        CurrentColorShip = _gameConfig.PlayerConfig.DefaultSkinShip,
                        UnlockedPlayerShips = new System.Collections.Generic.List<DataShipConfig>
                        {
                            _gameConfig.PlayerConfig.DefaultDataShip
                        }
                    },

                    LevelsData = new LevelsData
                    {
                        UnlockedLevels = new System.Collections.Generic.List<LevelData>
                            { _gameConfig.Levels.Levels[0] }
                    }
                };
            }

            Save();
        }

        public void ChangePlayerName(string name)
        {
            _gameSaveData.PlayerData.PlayerName = name;
            Save();
        }

        public void AddSkinPurchase(DataShipConfig skin)
        {
            if (!_gameSaveData.PlayerData.UnlockedPlayerShips.Contains(skin))
            {
                _gameSaveData.PlayerData.UnlockedPlayerShips.Add(skin);
                Save();
            }
        }

        public void AddStar(int stars)
        {
            _gameSaveData.PlayerData.PlayerStarScore += stars;
            Save();
        }

        public void SavePlayerTime(float time, LevelData levelData)
        {
            if (_gameSaveData.LevelsData.UnlockedLevels.Contains(levelData)) 
                _gameSaveData.LevelsData.UnlockedLevels.Find(l => l == levelData).BestTime = time;
            
            Save();
        }

        public void SavePlayerScore(int score)
        {
            _gameSaveData.PlayerData.PlayerScore = score;
        }

        public void AddUnlockLevel(LevelData levelData)
        {
            if (!_gameSaveData.LevelsData.UnlockedLevels.Contains(levelData))
                _gameSaveData.LevelsData.UnlockedLevels.Add(levelData);
            else
                Debug.LogError($"Exception: An already opened level has been added");
        }

        public void SaveColorShip(DataShipConfig shipConfig, string colorShip)
        {
            if (_gameSaveData.PlayerData.CurrentShip != shipConfig)
                _gameSaveData.PlayerData.CurrentShip = shipConfig;

            foreach (Skin skin in _gameSaveData.PlayerData.CurrentShip.ShipColors)
            {
                if (skin.ColorName == colorShip)
                {
                    _gameSaveData.PlayerData.CurrentColorShip = skin;
                    break;
                }
            }

            Save();
        }

        public void Save(DataShipConfig shipConfig, bool isNewShip)
        {
            if (isNewShip)
            {
                _gameSaveData.PlayerData.UnlockedPlayerShips.Add(shipConfig);
            }

            _gameSaveData.PlayerData.CurrentShip = shipConfig;
            Save();
        }

        public GameSaveData Load()
        {
            if (PlayerPrefs.HasKey(Constants.GameSave.PP_Key))
            {
                //string json = LocalStorage.GetKey(Constants.GameSave.YG_Key);
                string json = PlayerPrefs.GetString(Constants.GameSave.PP_Key);
                _gameSaveData = JsonUtility.FromJson<GameSaveData>(json);
                return _gameSaveData;
            }

            return null;
        }

        private void Save()
        {
            // var saveData = saveService.GetCurrentSave();
            //
            // if (saveData == null)    
            // {
            //     Debug.LogWarning("YgSaveAdapter: No save data to send");
            //     return;
            // }

            // Преобразуем в JSON для отправки в SDK
            string json = JsonUtility.ToJson(_gameSaveData, true);

            PlayerPrefs.SetString(Constants.GameSave.PP_Key, json);
            // Отправляем в локальное хранилище YG
            //LocalStorage.SetKey(Constants.GameSave.YG_Key, json);

            //Debug.Log("YgSaveAdapter: Save data sent to YG SDK");
            Debug.Log("Save data to PlayerPrefs");
        }

#if UNITY_EDITOR
        [MenuItem("Tools/Save Manager/AllClear")]
        private static void AllSaveClear()
        {
            PlayerPrefs.DeleteAll();
        }
#endif

        public void Clear()
        {
            PlayerPrefs.DeleteKey(Constants.GameSave.PP_Key);
        }

        public void AddCompletedLevel(LevelData level)
        {
            if (!_gameSaveData.LevelsData.UnlockedLevels.Contains(level))
            {
                _gameSaveData.LevelsData.UnlockedLevels.Add(level);
                Save();
            }
        }
    }
}