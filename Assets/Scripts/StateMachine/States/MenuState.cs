using Configs;
using Game;
using Managers;
using Players;
using UnityEngine;

namespace StateMachine.States
{
    public class MenuState : GameState
    {
        public MenuState(
            GameStateMachine stateMachine,
            GameConfig config,
            UIManager uiManager,
            AudioManager audioManager,
            InputManager inputManager,
            LevelManager levelManager,
            Player player)
            : base(stateMachine, config, uiManager, audioManager, inputManager, levelManager, player)
        {
        }

        public override void Enter()
        {
            UIManager.CloseAllWindows();
            UIManager.OpenWindow<MainMenuWindow>();
            //AudioManager.PlayMusic(AudioType.MenuMusic);

            // Освобождаем ресурсы игрового уровня
            LevelManager.UnloadCurrentLevel();
            Debug.Log("MenuState entered with full DI support!");
            Debug.Log("Entered Menu State");
        }

        public override void Update()
        {
            // Обработка ввода в меню
            // if (Input.GetKeyDown(KeyCode.Space))
            // {
            //     ChangeState<LoadingState>();
            // }
        }

        public override void Exit()
        {
            Debug.Log("Exited Menu State");
        }
    }
}