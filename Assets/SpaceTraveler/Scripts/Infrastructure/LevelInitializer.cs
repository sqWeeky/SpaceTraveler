using Reflex.Attributes;
using SpaceTraveler.Scripts.Managers;
using SpaceTraveler.Scripts.StateMachine;
using SpaceTraveler.Scripts.Windows;
using UnityEngine;

namespace SpaceTraveler.Scripts.Infrastructure
{
    public class LevelInitializer : MonoBehaviour
    {
        [Inject] private GameStateMachine _stateMachine;
        [Inject] private UIManager _uiManager;
        
        private PlayingWindow _playingWindow;

        private void Start()
        {
            _uiManager.CloseAllWindows();
            _playingWindow = _uiManager.OpenWindow<PlayingWindow>();
            
        }
    }
}
