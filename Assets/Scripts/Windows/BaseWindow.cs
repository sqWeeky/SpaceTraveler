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
        private void Inject(
            GameStateMachine stateMachine,
            UIManager uiManager,
            AudioManager audioManager)
        {
            Debug.LogError("В БАЗОВОМ ОКНЕ ПРОИЗОШЕЛ ИНЖЕКТ");
            GameStateMachine = stateMachine;
            UIManager = uiManager;
            AudioManager = audioManager;

            Debug.Log("BaseWindow INJECT");
        }

        public virtual void OnStart()
        {
            Debug.Log("OnStart()");
            Debug.Log(GameStateMachine != null);
            Debug.Log(UIManager != null);
            Debug.Log(AudioManager != null);
            Debug.Log(LevelManager != null);
        }

        public virtual void CloseWindow()
        {
        }
    }
}