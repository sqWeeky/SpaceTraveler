using Configs;
using Managers;
using Reflex.Core;
using StateMachine;
using UnityEngine;

namespace Infastracture
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        [Header("Configs")] 
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private UIManagerConfig _uiManagerConfig;
        [SerializeField] private LevelsConfig _levelsConfig;
        
        public void InstallBindings(ContainerBuilder builder)
        {
            // === ScriptableObjects ===
            builder.AddSingleton(_gameConfig);
            builder.AddSingleton(_uiManagerConfig);
            builder.AddSingleton(_levelsConfig);

            // === Managers ===
            builder.AddSingleton(AudioManager.Create());
            builder.AddSingleton(UIManager.Create());
            
            builder.AddSingleton(InputManager.Create());
            builder.AddSingleton(LevelManager.Create());

            // === Core Game Systems ===
            builder.AddSingleton(GameStateMachine.Create());

            Debug.Log("ProjectInstaller: All dependencies registered");
        }
    }
}