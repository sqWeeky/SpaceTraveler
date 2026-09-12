using SpaceTraveler.Scripts.Camera;
using SpaceTraveler.Scripts.MenuLevels;
using UnityEngine;

namespace SpaceTraveler.Scripts.Windows
{
    public class LevelsMenuWindow : BaseWindow
    {
        [SerializeField] private SplineButtonPlacer _splineButtonPlacer;
        [SerializeField] private CameraDragHandler _cameraDragHandler;
        
        private void Start()
        {
            if (!_splineButtonPlacer.Initialized && !_cameraDragHandler.Initialized)
            {
                _splineButtonPlacer.Init();
                _cameraDragHandler.Init(_splineButtonPlacer.LevelButtons);
            }
        }

        public override void CloseWindow()
        {
            UIManager.CloseWindow<LevelsMenuWindow>();
        }
    }
}
