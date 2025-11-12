using StateMachine;
using UnityEngine;

namespace Managers
{
    public abstract class BaseWindow : MonoBehaviour
    {
        protected GameStateMachine GameStateMachine { get; private set; }
        protected UIManager UIManager { get; private set; }
        protected AudioManager AudioManager { get; private set; }

        public void InjectDependencies(GameStateMachine stateMachine,
            UIManager uiManager,
            AudioManager audioManager)
        {
            GameStateMachine = stateMachine;
            UIManager = uiManager;
            AudioManager = audioManager;
        }

        public virtual void CloseWindow()
        {
        }
    }
}