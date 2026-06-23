using SpaceTraveler.Scripts.StateMachine.States;

namespace SpaceTraveler.Scripts.Windows
{
    public class LevelsMenuWindow : BaseWindow
    {
        private void OnEnable()
        {
            UIManager.CloseAllWindows();
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
