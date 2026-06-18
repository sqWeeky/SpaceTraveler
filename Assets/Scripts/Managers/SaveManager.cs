using Configs;
using Configs.Skins;
using Save;
using UnityEngine;
using YG.Utils;

namespace Managers
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
        private const string PP_KEY = "SpaceTraveler_GameSave";
        private const string YG_KEY = "SpaceTraveler_YgSave";
        
        private GameSaveData _gameSaveData;

        public void Save()
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
            
            PlayerPrefs.SetString(PP_KEY, json);
            // Отправляем в локальное хранилище YG
            //LocalStorage.SetKey("YG_KEY", json);
            
            Debug.Log("YgSaveAdapter: Save data sent to YG SDK");
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey(PP_KEY))
            {
                //string json = LocalStorage.GetKey("YG_KEY");
                string json = PlayerPrefs.GetString(PP_KEY);
                _gameSaveData = JsonUtility.FromJson<GameSaveData>(json);
            }
        }

        public void Clear()
        {
            // Реализовать удаление файла сохранения
        }
        
        public void AddCompletedLevel(LevelsConfig.LevelData level)
        {
            if (!_gameSaveData.LevelsData.UnlockedLevels.Contains(level))
            {
                _gameSaveData.LevelsData.UnlockedLevels.Add(level);
                Save();
            }
        }

        public void AddSkinPurchase(DataShipConfig skin)
        {
            if (!_gameSaveData.PlayerData.UnlockedPlayerShips.Contains(skin))
            {
                _gameSaveData.PlayerData.UnlockedPlayerShips.Add(skin);
                Save();
            }
        }
    }
}








