using SpaceTraveler.Scripts.StateMachine.States;
using SpaceTraveler.Scripts.Systems;
using UnityEngine;

namespace SpaceTraveler.Scripts.Windows
{
    public class PlayingWindow : BaseWindow
    {
        [SerializeField] private TimerCounter _timer;

        public TimerCounter Timer => _timer;

        private void Start()
        {
            _timer.Init(LevelManager.GetCurrentLevelData());
        }

        public void PauseOpen()
        {
            GameStateMachine.ChangeState<PausedState>();
        }

        public void UpdateGameTimer(float gameTime)
        {
        }
    }
}