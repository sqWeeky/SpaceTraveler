using Configs;
using UnityEngine;

namespace StateMachine.States
{
    public class LevelCompleteState : GameState
    {
        public LevelCompleteState(GameStateMachine stateMachine, GameConfig config) 
            : base(stateMachine, config) { }

        public override void Enter()
        {
            Config.UIManager.ShowLevelCompleteScreen();
            //_context.AudioManager.PlaySFX(AudioType.LevelComplete);
        
            // Начисляем награды
            var starsEarned = CalculateStars();
            //_context.Player.Progress.AddStars(starsEarned);
        
            Debug.Log("Entered LevelComplete State");
        }

        public override void Update()
        {
            if (Config.InputManager.WasContinuePressed)
            {
                // Загружаем следующий уровень или возвращаем в меню
                if (Config.LevelManager.HasNextLevel)
                {
                    ChangeState<LoadingState>();
                }
                else
                {
                    ChangeState<MenuState>();
                }
            }
        }

        public override void Exit()
        {
            Config.UIManager.HideLevelCompleteScreen();
            Debug.Log("Exited LevelComplete State");
        }
    
        private int CalculateStars()
        {
            // Логика расчета звезд за уровень
            return 3; // пример
        }
    }
}