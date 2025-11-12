using Configs;
using Game;
using Managers;
using Players;
using Reflex.Core;
using UnityEngine;

namespace StateMachine.States
{
    public class MenuState : GameState
    {
        // public MenuState(Container container) : base(container)
        // {
        // }

        public override void Enter()
        {
            // Container.Resolve<UIManager>().CloseAllWindows();
             Container.ProjectContainer.Resolve<UIManager>().OpenWindow<MainMenuWindow>();
            //UIManager.OpenWindow<MainMenuWindow>();
            //AudioManager.PlayMusic(AudioType.MenuMusic);

            // Освобождаем ресурсы игрового уровня
            //Container.Resolve<LevelManager>().UnloadCurrentLevel();
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