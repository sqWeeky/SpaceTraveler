using SpaceTraveler.Scripts.Windows;
using UnityEngine;

namespace SpaceTraveler.Scripts.StateMachine.States
{
    public class MenuState : GameState
    {
        public override void Enter()
        {
            Time.timeScale = 1f;
            Debug.Log("Entered Menu State");
            
            UIManager.CloseAllWindows();
            UIManager.OpenWindow<MainMenuWindow>();

            //AudioManager.PlayMusic(AudioType.MenuMusic);
        }
        
        public override void Exit()
        {
            Debug.Log("Exited Menu State");
        }
    }
}