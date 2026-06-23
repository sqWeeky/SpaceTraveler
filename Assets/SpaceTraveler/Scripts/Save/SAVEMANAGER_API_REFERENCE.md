# SaveManager — Справочник по использованию

## Быстрый старт

### 1. Регистрация (уже сделана в `ProjectInstaller.cs`)
```csharp
builder.AddSingleton(SaveManager.Create());
```

### 2. Использование в Game States
```csharp
public class MyGameState : GameState
{
    public override void Enter()
    {
        // SaveManager автоматически инжектирован через [Inject]
        SaveManager.Load();      // Загрузить данные
        SaveManager.Save();      // Сохранить данные
    }
}
```

---

## API Reference

### Основные методы

#### `void Save()`
Сохраняет текущие данные в JSON файл.
```csharp
SaveManager.Save();
// Файл: Application.persistentDataPath/space_traveler_save.json
```

#### `void Load()`
Загружает данные из файла или создаёт новое сохранение, если файл отсутствует.
```csharp
SaveManager.Load();
// Вызывается в MenuState при старте игры
```

#### `void Clear()`
Удаляет файл сохранения и возвращает игру в дефолтное состояние.
```csharp
SaveManager.Clear();
// Вызывается при "Новая игра" или "Удалить прогресс"
```

#### `GameSaveSnapshot GetSnapshot()`
Возвращает текущий снимок данных (DTO) для отправки в SDK или UI.
```csharp
var snapshot = SaveManager.GetSnapshot();
Debug.Log($"Score: {snapshot.Score}, Lives: {snapshot.Lives}");
// Можно отправить snapshot в SDK, Analytics, UI и т.д.
```

---

### Методы для работы с очками и жизнями

#### `void AddScore(int amount)`
Добавляет очки к текущему счёту.
```csharp
SaveManager.AddScore(100);  // +100 очков
// Затем вызвать SaveManager.Save() для сохранения
```

#### `void SetLives(int lives)`
Устанавливает количество жизней.
```csharp
SaveManager.SetLives(3);
SaveManager.Save();
```

---

### Методы для работы с уровнями

#### `void AddCompletedLevel(string levelId)`
Добавляет уровень в список пройденных.
```csharp
var currentLevel = LevelManager.GetCurrentLevelData();
SaveManager.AddCompletedLevel(currentLevel.LevelId);
SaveManager.Save();
// Вызывается в LevelCompleteState
```

#### `bool IsLevelCompleted(string levelId)`
Проверяет, был ли уровень пройден.
```csharp
if (SaveManager.IsLevelCompleted("level_1"))
{
    // Уровень уже пройден
}
// Используется при отображении меню уровней
```

---

### Методы для работы со скинами

#### `void SetSelectedSkin(string skinId)`
Устанавливает выбранный скин.
```csharp
SaveManager.SetSelectedSkin("skin_red");
SaveManager.Save();
```

#### `void AddSkinPurchase(string skinId)`
Добавляет запись о покупке скина с автоматической временной меткой (unix миллисекунды).
```csharp
SaveManager.AddSkinPurchase("skin_gold");
SaveManager.Save();
// Time: автоматически DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
```

#### `long? GetSkinPurchaseTime(string skinId)`
Получает время покупки скина (unix миллисекунды). Возвращает `null`, если скин не куплен.
```csharp
var purchaseTime = SaveManager.GetSkinPurchaseTime("skin_gold");
if (purchaseTime != null)
{
    Debug.Log($"Куплено: {purchaseTime}");
}
```

---

## Структура данных (GameSaveSnapshot)

```csharp
public class GameSaveSnapshot
{
    // Игровая статистика
    public int Score;              // Очки
    public int Lives;              // Жизни
    
    // Скины
    public string SelectedSkinId;  // ID выбранного скина
    public List<SkinPurchaseRecord> SkinPurchases;  // История покупок
    
    // Уровни
    public List<string> CompletedLevelIds;  // Пройденные уровни
    public List<LevelStarRecord> LevelStars; // Звёзды по уровням
    
    // Метаданные
    public int SaveVersion = 1;
    public long SaveTimestamp;     // unix миллисекунды
    public string PlayerName = "Player";
}
```

---

## Примеры использования

### Пример 1: Загрузка при старте игры (MenuState)
```csharp
public class MenuState : GameState
{
    public override void Enter()
    {
        // Загружаем сохранённые данные
        SaveManager.Load();
        
        // Получаем текущие данные для UI
        var snapshot = SaveManager.GetSnapshot();
        Debug.Log($"Score: {snapshot.Score}, Levels: {snapshot.CompletedLevelIds.Count}");
        
        UIManager.OpenWindow<MainMenuWindow>();
    }
}
```

### Пример 2: Сохранение при завершении уровня (LevelCompleteState)
```csharp
public class LevelCompleteState : GameState
{
    public override void Enter()
    {
        var currentLevel = LevelManager.GetCurrentLevelData();
        
        // Добавляем уровень в список пройденных
        SaveManager.AddCompletedLevel(currentLevel.LevelId);
        
        // Сохраняем в файл
        SaveManager.Save();
        
        Debug.Log("Уровень сохранён!");
    }
}
```

### Пример 3: Покупка скина (ShopWindow)
```csharp
// Когда игрок нажимает "Купить"
SaveManager.AddSkinPurchase("skin_gold");
SaveManager.SetSelectedSkin("skin_gold");
SaveManager.Save();

Debug.Log("Скин куплен и установлен!");
```

### Пример 4: Отправка данных в SDK
```csharp
public void SendToSDK()
{
    var snapshot = SaveManager.GetSnapshot();
    
    var payload = new {
        score = snapshot.Score,
        lives = snapshot.Lives,
        completedLevels = snapshot.CompletedLevelIds.Count,
        selectedSkin = snapshot.SelectedSkinId,
        purchases = snapshot.SkinPurchases
    };
    
    SDK.SendPlayerData(payload);
}
```

### Пример 5: Сброс прогресса
```csharp
// Меню: "Новая игра"
SaveManager.Clear();
// Перезагружаем сцену или переходим в MenuState
```

---

## События (Опционально)

SaveManager генерирует события при загрузке и сохранении:

```csharp
SaveManager.OnGameSaved += () => Debug.Log("Игра сохранена!");
SaveManager.OnGameLoaded += () => Debug.Log("Игра загружена!");
```

---

## Точка хранения файла

- **Путь:** `Application.persistentDataPath + "/space_traveler_save.json"`
- **На разных платформах:**
  - **Android:** `/data/data/com.yourcompany.game/files/space_traveler_save.json`
  - **iOS:** `Documents/space_traveler_save.json`
  - **PC (Editor):** `C:\Users\YourName\AppData\LocalLow\DefaultCompany\SpaceTraveler\space_traveler_save.json`
  - **WebGL:** Browser LocalStorage

---

## Важные замечания

1. **Главный поток:** Все операции Save/Load должны вызываться на главном Unity потоке (из Game States, меню и т.д.).

2. **Формат JSON:** Используется встроенный `JsonUtility`. Все классы должны быть `[Serializable]`.

3. **Версионирование:** `SaveVersion` помогает отслеживать изменения структуры данных. При миграции можно добавить checks.

4. **Синхронизация:** SaveManager держит данные в памяти (`_currentSnapshot`). Вывызывайте `Save()` после изменений!

5. **Отладка:** Проверяйте файл сохранения вручную:
   - Откройте `Application.persistentDataPath` в файл-менеджере
   - Отредактируйте `space_traveler_save.json` текстовым редактором

---

## Контрольный список интеграции

- [x] `SaveManager.cs` создан в `Assets/Scripts/Managers/`
- [x] `GameSaveSnapshot.cs` создан в `Assets/Scripts/Save/`
- [x] `SaveManager` зарегистрирован в `ProjectInstaller.cs`
- [x] `SaveManager` добавлен в `GameState` как `[Inject]`
- [x] `MenuState` загружает данные в `Enter()`
- [x] `LevelCompleteState` сохраняет уровень в `Enter()`
- [ ] Добавить сохранение в другие места (покупка, смена скина и т.д.)
- [ ] Интегрировать с SDK (используя `GetSnapshot()`)
- [ ] Добавить UI для отображения прогресса
- [ ] Протестировать загрузку/сохранение на целевых платформах

