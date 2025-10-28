using UnityEngine;

namespace Infastracture
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}