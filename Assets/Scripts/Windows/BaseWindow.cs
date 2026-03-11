using Managers;
using Reflex.Attributes;
using StateMachine;
using UnityEngine;

namespace Windows
{
    public abstract class BaseWindow : MonoBehaviour
    {
        protected GameStateMachine GameStateMachine { get; private set; }
        protected UIManager UIManager { get; private set; }
        protected AudioManager AudioManager { get; private set; }
        protected LevelManager LevelManager { get; private set; }

        [Inject]
        public void Inject(
            GameStateMachine stateMachine,
            UIManager uiManager,
            AudioManager audioManager, 
            LevelManager levelManager)
        {
            GameStateMachine = stateMachine;
            UIManager = uiManager;
            AudioManager = audioManager;
            LevelManager = levelManager;
        }

        public virtual void CloseWindow()
        {
        }
    }
}