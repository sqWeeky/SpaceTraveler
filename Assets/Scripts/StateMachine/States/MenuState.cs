using Configs;
using UnityEngine;

namespace StateMachine.States
{
    public class MenuState : GameState
    {
        public MenuState(GameStateMachine stateMachine, GameConfig config) 
            : base(stateMachine, config) { }

        public override void Enter()
        {
            Config.UIManager.ShowMenuScreen();
            //_context.AudioManager.PlayMusic(AudioType.MenuMusic);
        
            // Освобождаем ресурсы игрового уровня
            Config.LevelManager.UnloadCurrentLevel();
        
            Debug.Log("Entered Menu State");
        }

        public override void Update()
        {
            // Обработка ввода в меню
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeState<LoadingState>();
            }
        }

        public override void Exit()
        {
            Config.UIManager.HideMenuScreen();
            Debug.Log("Exited Menu State");
        }
    }
}