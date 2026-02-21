using Managers;
using StateMachine.States;

public class LevelsMenuWindow : BaseWindow
{
    public void OnStartGame(string sceneName)
    {
        LevelManager.LoadLevel(sceneName); // Загружаем конкретный уровень
        GameStateMachine.ChangeState<LoadingState>(); // Переходим в состояние загрузки
    }

    public void OnBack()
    {
        UIManager.CloseWindow<LevelsMenuWindow>();
    }
}
