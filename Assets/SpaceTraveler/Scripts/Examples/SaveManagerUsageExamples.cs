// using Managers;
// using Reflex.Attributes;
// using UnityEngine;
//
// namespace Examples
// {
//     /// <summary>
//     /// ПРИМЕРЫ ИСПОЛЬЗОВАНИЯ SaveManager
//     /// 
//     /// Этот файл содержит примеры того, как использовать SaveManager в разных сценариях:
//     /// 1. Сохранение при завершении уровня (уже реализовано в LevelCompleteState)
//     /// 2. Загрузка при старте игры (уже реализовано в MenuState)
//     /// 3. Монетизация: сохранение покупок скинов
//     /// 4. Обновление прогресса во время игры
//     /// 5. Отправка данных в SDK
//     /// </summary>
//     public class SaveManagerUsageExamples : MonoBehaviour
//     {
//         // Инжектируем SaveManager через Reflex
//         [Inject] private SaveManager _saveManager;
//
//         // ====================================
//         // ПРИМЕР 1: Сохранение при покупке скина
//         // ====================================
//         /// <summary>
//         /// Вызывается когда игрок покупает скин (например, из ShopWindow).
//         /// </summary>
//         public void OnSkinPurchased(string skinId)
//         {
//             Debug.Log($"[ShopWindow] Player purchased skin: {skinId}");
//             
//             // 1. Добавляем запись о покупке (с временной меткой)
//             _saveManager.AddSkinPurchase(skinId);
//             
//             // 2. Если нужно, устанавливаем её как текущую
//             _saveManager.SetSelectedSkin(skinId);
//             
//             // 3. Сохраняем в файл
//             _saveManager.Save();
//             
//             Debug.Log("[ShopWindow] Purchase saved successfully");
//         }
//
//         // ====================================
//         // ПРИМЕР 2: Обновление очков во время игры
//         // ====================================
//         /// <summary>
//         /// Вызывается когда игрок собирает что-то ценное или побеждает врага.
//         /// Сохраняем прогресс периодически.
//         /// </summary>
//         public void OnScoreGained(int points)
//         {
//             Debug.Log($"[GameplayManager] Score gained: +{points}");
//             
//             // 1. Добавляем очки в SaveManager
//             _saveManager.AddScore(points);
//             
//             // 2. Опционально: сохраняем каждые N очков (чтобы не писать в файл каждый кадр)
//             var snapshot = _saveManager.GetSnapshot();
//             if (snapshot.Score % 100 == 0)  // Сохраняем каждые 100 очков
//             {
//                 _saveManager.Save();
//                 Debug.Log("[GameplayManager] Checkpoint saved");
//             }
//         }
//
//         // ====================================
//         // ПРИМЕР 3: Получение данных для отправки в SDK
//         // ====================================
//         /// <summary>
//         /// Вызывается перед отправкой данных в аналитику или SDK.
//         /// SaveManager предоставляет единую точку доступа ко всем данным.
//         /// </summary>
//         public void SendProgressToSDK()
//         {
//             Debug.Log("[SDKManager] Preparing data for SDK...");
//             
//             // 1. Получаем снимок (DTO)
//             var saveSnapshot = _saveManager.GetSnapshot();
//             
//             // 2. Формируем payload для SDK
//             var sdkPayload = new SDKProgressPayload
//             {
//                 PlayerId = "user_123",  // Или получить из другого менеджера
//                 Score = saveSnapshot.Score,
//                 Lives = saveSnapshot.Lives,
//                 SelectedSkin = saveSnapshot.SelectedSkinId,
//                 CompletedLevels = saveSnapshot.CompletedLevelIds.Count,
//                 TotalPlayTime = Time.time,
//                 SaveVersion = saveSnapshot.SaveVersion,
//                 Timestamp = saveSnapshot.SaveTimestamp
//             };
//             
//             // 3. Отправляем в SDK (мокирована функция)
//             SendToSDK(sdkPayload);
//             
//             Debug.Log("[SDKManager] Data sent to SDK");
//         }
//
//         // ====================================
//         // ПРИМЕР 4: Проверка, был ли уровень пройден
//         // ====================================
//         /// <summary>
//         /// Вызывается при отображении меню уровней.
//         /// Проверяем, какие уровни уже пройдены.
//         /// </summary>
//         public void UpdateLevelMenu()
//         {
//             Debug.Log("[LevelMenuWindow] Updating level display...");
//             
//             // Пример: получаем список уровней из конфига
//             // var allLevels = levelsConfig.Levels;
//             
//             // for (int i = 0; i < allLevels.Length; i++)
//             // {
//             //     bool isCompleted = _saveManager.IsLevelCompleted(allLevels[i].LevelId);
//             //     bool isLocked = !isCompleted && i > 0;  // Запираем уровни, пока не пройдены предыдущие
//             //     
//             //     UpdateLevelButton(allLevels[i], isCompleted, isLocked);
//             // }
//             
//             Debug.Log("[LevelMenuWindow] Level display updated");
//         }
//
//         // ====================================
//         // ПРИМЕР 5: Получение времени покупки скина
//         // ====================================
//         /// <summary>
//         /// Вызывается когда нужно узнать, когда был куплен скин.
//         /// Полезно для: ограничения новых скинов, подсчёта времени, аналитики.
//         /// </summary>
//         public void CheckSkinPurchaseTime(string skinId)
//         {
//             Debug.Log($"[ShopWindow] Checking purchase time for skin: {skinId}");
//             
//             // 1. Получаем время покупки (unix миллисекунды)
//             var purchaseTimeMs = _saveManager.GetSkinPurchaseTime(skinId);
//             
//             if (purchaseTimeMs == null)
//             {
//                 Debug.Log($"[ShopWindow] Skin {skinId} not purchased");
//                 return;
//             }
//             
//             // 2. Преобразуем в DateTime для анализа
//             var purchaseTime = UnixTimeStampToDateTime(purchaseTimeMs.Value);
//             var daysSincePurchase = (System.DateTime.UtcNow - purchaseTime).TotalDays;
//             
//             Debug.Log($"[ShopWindow] Skin purchased {daysSincePurchase:F1} days ago at {purchaseTime}");
//             
//             // 3. Пример: если скин куплен давно, показываем уведомление
//             if (daysSincePurchase > 30)
//             {
//                 Debug.Log($"[ShopWindow] This skin has been owned for over a month!");
//             }
//         }
//
//         // ====================================
//         // ПРИМЕР 6: Сброс сохранения (Game Over / Новая игра)
//         // ====================================
//         /// <summary>
//         /// Вызывается из меню: "Новая игра", "Удалить прогресс", "Выход".
//         /// </summary>
//         public void ResetGameProgress()
//         {
//             Debug.Log("[MenuWindow] Resetting game progress...");
//             
//             // 1. Сбрасываем SaveManager
//             _saveManager.Clear();
//             
//             // 2. Если нужно, перезагружаем сцену или возвращаемся в меню
//             Debug.Log("[MenuWindow] Game progress reset");
//         }
//
//         // ====================================
//         // ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ
//         // ====================================
//
//         private System.DateTime UnixTimeStampToDateTime(long timeStampMs)
//         {
//             var dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
//             dateTime = dateTime.AddMilliseconds(timeStampMs).ToLocalTime();
//             return dateTime;
//         }
//
//         private void SendToSDK(SDKProgressPayload payload)
//         {
//             // TODO: Реальная отправка в SDK
//             // SDK.SendProgress(payload);
//             Debug.Log($"[SDK] Sending progress: Score={payload.Score}, Lives={payload.Lives}");
//         }
//
//         // ====================================
//         // КЛАССЫ ДЛЯ ПРИМЕРОВ
//         // ====================================
//
//         private class SDKProgressPayload
//         {
//             public string PlayerId;
//             public int Score;
//             public int Lives;
//             public string SelectedSkin;
//             public int CompletedLevels;
//             public float TotalPlayTime;
//             public int SaveVersion;
//             public long Timestamp;
//         }
//     }
// }
//
