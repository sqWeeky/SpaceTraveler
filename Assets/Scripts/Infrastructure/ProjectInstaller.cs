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
            builder.AddSingleton(_gameConfig);     
            builder.AddSingleton(_uiManagerConfig); 
            builder.AddSingleton(_levelsConfig);   
            
            builder.AddSingleton(UIManager.Create());
            builder.AddSingleton(AudioManager.Create());
            builder.AddSingleton(LevelManager.Create());
            builder.AddSingleton(InputManager.Create());
            
            builder.AddSingleton(GameStateMachine.Create());
            
            Debug.Log("ProjectInstaller: All dependencies registered");
        }
    }
}