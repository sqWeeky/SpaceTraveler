using Reflex.Attributes;
using Reflex.Extensions;
using Reflex.Injectors;
using SpaceTraveler.Scripts.Managers;
using UnityEngine.SceneManagement;

namespace SpaceTraveler.Scripts.StateMachine.States
{
    public abstract class GameState : IGameState
    {
        protected GameStateMachine GameStateMachine { get; private set; }
        protected UIManager UIManager { get; private set; }
        protected AudioManager AudioManager { get; private set; }
        protected InputManager InputManager { get; private set; }
        protected LevelManager LevelManager { get; private set; }
        protected SaveManager SaveManager { get; private set; }

        public GameState Init(
            UIManager uiManager, AudioManager audioManager, InputManager inputManager, LevelManager levelManager,
            SaveManager saveManager)
        {
            UIManager = uiManager;
            AudioManager = audioManager;
            InputManager = inputManager;
            LevelManager = levelManager;
            SaveManager = saveManager;

            return this;
        }

        public virtual void Enter()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Exit()
        {
        }

        protected void ChangeState<T>() where T : GameState
        {
            GameStateMachine.ChangeState<T>();
        }
    }
}