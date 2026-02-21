using Managers;
using UnityEngine;

namespace StateMachine.States
{
    public class LevelCompleteState : GameState
    {
        public LevelCompleteState(GameStateMachine gameStateMachine, UIManager uiManager, AudioManager audioManager,
            InputManager inputManager, LevelManager levelManager) : base(gameStateMachine, uiManager, audioManager,
            inputManager, levelManager)
        {
        }

        public override void Enter()
        {
            //GameRoot.Instance.GetManager<UIManager>().ShowLevelCompleteScreen();
            //_context.AudioManager.PlaySFX(AudioType.LevelComplete);

            // Начисляем награды
            var starsEarned = CalculateStars();
            //_context.Player.Progress.AddStars(starsEarned);

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