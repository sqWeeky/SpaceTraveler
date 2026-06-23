using SpaceTraveler.Scripts.Windows;
using UnityEngine;

namespace SpaceTraveler.Scripts.StateMachine.States
{
    public class MenuState : GameState
    {
       public override void Enter()
        {
            base.Enter();
            
            // === ПРИМЕР ИСПОЛЬЗОВАНИЯ SaveManager ===
            // 1. Загружаем сохранённые данные при входе в меню
            //SaveManager.Load();
            
            // 2. Если нужно, можем получить снимок для отправки в SDK
            // var snapshot = SaveManager.GetSnapshot();
            // Debug.Log($"Current Score: {snapshot.Score}, Lives: {snapshot.Lives}");
            
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