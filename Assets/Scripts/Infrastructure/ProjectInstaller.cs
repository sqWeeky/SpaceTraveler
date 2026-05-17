using Configs;
using Managers;
using Reflex.Core;
using StateMachine;
using UnityEngine;

namespace Infrastructure
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        [Header("Configs")] 
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private UIManagerConfig _uiManagerConfig;
        [SerializeField] private LevelsConfig _levelsConfig;
        
        public void InstallBindings(ContainerBuilder builder)
        {
            // Проверяем конфиги
            if (_gameConfig == null)
            {
                Debug.LogError($"[ProjectInstaller] GameConfig is not assigned in inspector on {gameObject.name}!", this);
                return;
            }
    
            if (_uiManagerConfig == null)
            {
                Debug.LogError($"[ProjectInstaller] UIManagerConfig is not assigned in inspector on {gameObject.name}!", this);
                return;
            }
    
            if (_levelsConfig == null)
            {
                Debug.LogError($"[ProjectInstaller] LevelsConfig is not assigned in inspector on {gameObject.name}!", this);
                return;
            }
            
            // === ScriptableObjects ===
            // === ScriptableObjects - используем RegisterValue ===
            builder.AddSingleton(_gameConfig);      // Исправлено
            builder.AddSingleton(_uiManagerConfig); // Исправлено
            builder.AddSingleton(_levelsConfig);    // Исправлено
            //
            // var uiManager = UIManager.Create();
            // var audioManager = AudioManager.Create();
            // var inputManager = InputManager.Create();
            // var levelManager = LevelManager.Create();
            //
            // Debug.LogError(uiManager == null);
            // // === Managers ===
            // // Если Create() возвращает уже созданный объект
            builder.AddSingleton(UIManager.Create());
            builder.AddSingleton(AudioManager.Create());
            builder.AddSingleton(LevelManager.Create());
            builder.AddSingleton(InputManager.Create());
            //
             builder.AddSingleton(GameStateMachine.Create());
            
            Debug.Log("ProjectInstaller: All dependencies registered");
        }
    }
}