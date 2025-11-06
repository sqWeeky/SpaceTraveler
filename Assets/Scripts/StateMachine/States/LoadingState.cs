using Configs;
using Game;
using Managers;
using Players;
using UnityEngine;

namespace StateMachine.States
{
    public class LoadingState : GameState
    {
        private AsyncOperation _loadingOperation;

        public LoadingState(
            GameStateMachine stateMachine, 
            GameConfig config, 
            UIManager uiManager, 
            AudioManager audioManager, 
            InputManager inputManager, 
            LevelManager levelManager, 
            Player player) : 
            base(stateMachine, config, uiManager, audioManager, inputManager, levelManager, player)
        {
        }

        public override void Enter()
        {
            //GameRoot.Instance.GetManager<UIManager>().ShowLoadingScreen();
        
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
            //GameRoot.Instance.GetManager<UIManager>().HideLoadingScreen();
            Debug.Log("Exited Loading State");
        }
    }
}