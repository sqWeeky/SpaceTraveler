using Windows;
using Managers;
using UnityEngine;

namespace StateMachine.States
{
    public class MenuState : GameState
    {
       public override void Enter()
        {
            base.Enter();
            UIManager.CloseAllWindows();
            UIManager.OpenWindow<MainMenuWindow>();

            //AudioManager.PlayMusic(AudioType.MenuMusic);


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