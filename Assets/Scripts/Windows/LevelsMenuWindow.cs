using System;
using StateMachine.States;

namespace Windows
{
    public class LevelsMenuWindow : BaseWindow
    {
        private void OnEnable()
        {
            UIManager.CloseWindow<GameModeWindow>();
        }

        public void OnStartGame(string sceneName)
        {
            GameStateMachine.ChangeState<LoadingState>(); // Переходим в состояние загрузки
            LevelManager.LoadLevel(sceneName); // Загружаем конкретный уровень
        }

        public void OnBack()
        {
            UIManager.OpenWindow<GameModeWindow>();
            UIManager.CloseWindow<LevelsMenuWindow>();
        }
    }
}
