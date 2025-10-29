namespace Managers
{
    public class InputManager : BaseManager
    {
        private bool _wasPausePressed;
        private bool _wasResumePressed;
        private bool _anyInput;
        private bool _wasMenuRequested;
        private bool _wasContinuePressed;

        public bool WasPausePressed => _wasPausePressed;
        public bool WasResumePressed => _wasResumePressed;
        public bool AnyInput => _anyInput;
        public bool WasMenuRequested => _wasMenuRequested;
        public bool WasContinuePressed => _wasContinuePressed;

        public void DisableGameplayInput()
        {
            
        }

        public void EnableGameplayInput()
        {
            
        }
    }
}