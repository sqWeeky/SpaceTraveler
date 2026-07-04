using Reflex.Core;
using SpaceTraveler.Scripts.Configs;
using SpaceTraveler.Scripts.Managers;
using SpaceTraveler.Scripts.StateMachine;
using UnityEngine;

namespace SpaceTraveler.Scripts.Infrastructure
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