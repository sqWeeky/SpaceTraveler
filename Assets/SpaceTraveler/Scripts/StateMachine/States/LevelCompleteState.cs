using UnityEngine;

namespace SpaceTraveler.Scripts.StateMachine.States
{
    public class LevelCompleteState : GameState
    {
        public override void Enter()
        {
            //GameRoot.Instance.GetManager<UIManager>().ShowLevelCompleteScreen();
            //_context.AudioManager.PlaySFX(AudioType.LevelComplete);

            // Начисляем награды
            var starsEarned = CalculateStars();
            //_context.Player.Progress.AddStars(starsEarned);

            // === ПРИМЕР ИСПОЛЬЗОВАНИЯ SaveManager ===
            // 1. Получаем текущий уровень из LevelManager
            var currentLevel = LevelManager.GetCurrentLevelData();
            if (currentLevel != null)
            {
                // 2. Добавляем уровень в список пройденных
                //SaveManager.AddCompletedLevel(currentLevel.LevelId);
                
                // 3. Можем добавить звёзды, если нужна система рейтинга
                // SaveManager.AddStars(currentLevel.LevelId, starsEarned);
                
                Debug.Log($"[LevelCompleteState] Level completed: {currentLevel.LevelId}, Stars: {starsEarned}");
            }
            
            // 4. Сохраняем игру (записываем в файл)
           // SaveManager.Save();
            
            // 5. Опционально: отправляем снимок в SDK или аналитику
            // var saveSnapshot = SaveManager.GetSnapshot();
            // Analytics.LogLevelComplete(saveSnapshot);

            Debug.Log("Entered LevelComplete State");
        }

        public override void Update()
        {
            // if (Container.Resolve<InputManager>().WasContinuePressed)
            // {
            //     // Загружаем следующий уровень или возвращаем в меню
            //     if (Container.Resolve<LevelManager>().HasNextLevel)
            //     {
            //         ChangeState<LoadingState>();
            //     }
            //     else
            //     {
            //         ChangeState<MenuState>();
            //     }
            // }
        }

        public override void Exit()
        {
            //GameRoot.Instance.GetManager<UIManager>().HideLevelCompleteScreen();
            Debug.Log("Exited LevelComplete State");
        }

        private int CalculateStars()
        {
            // Логика расчета звезд за уровень
            return 3; // пример
        }
    }
}