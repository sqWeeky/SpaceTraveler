using StateMachine;
using UnityEngine;

namespace Managers
{
    public abstract class BaseWindow : MonoBehaviour
    {
        protected IGameStateMachine GameStateMachine { get; private set; }
        protected UIManager UIManager { get; private set; }
        protected AudioManager AudioManager { get; private set; }

        public virtual void InjectDependencies(IGameStateMachine stateMachine,
            UIManager uiManager, 
            AudioManager audioManager)
        {
            GameStateMachine = stateMachine;
            UIManager = uiManager;
            AudioManager = audioManager;
        }
        
        public virtual void CloseWindow() { }
    }
}