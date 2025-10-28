using Configs;
using UnityEngine;

namespace StateMachine.States
{
    public class LoadingState : GameState
    {
        private AsyncOperation _loadingOperation;
    
        public LoadingState(GameStateMachine stateMachine, GameConfig config) 
            : base(stateMachine, config) { }

        public override void Enter()
        {
            Config.UIManager.ShowLoadingScreen();
        
            // Запускаем асинхронную загрузку
            //_loadingOperation = _context.LevelManager.LoadNextLevelAsync();
        
            Debug.Log("Entered Loading State");
        }

        public override void Update()
        {
            if (_loadingOperation != null && _loadingOperation.isDone)
            {
                // Переходим в игровое состояние после загрузки
                ChangeState<PlayingState>();
            }
        }

        public override void Exit()
        {
            Config.UIManager.HideLoadingScreen();
            Debug.Log("Exited Loading State");
        }
    }
}