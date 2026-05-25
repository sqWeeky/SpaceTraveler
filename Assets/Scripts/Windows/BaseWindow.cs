using Configs;
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
        protected GameConfig GameConfig { get; private set; }
        protected AudioManager AudioManager { get; private set; }
        protected LevelManager LevelManager { get; private set; }


        [Inject]
        private void Inject(
            GameStateMachine stateMachine,
            UIManager uiManager,
            GameConfig gameConfig,
            AudioManager audioManager)
        {
            GameStateMachine = stateMachine;
            UIManager = uiManager;
            GameConfig = gameConfig;
            AudioManager = audioManager;

            Debug.Log("BaseWindow INJECT");
        }

        public virtual void OnStart()
        {
        }

        public virtual void CloseWindow()
        {
        }
    }
}