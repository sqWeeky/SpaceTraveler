using Reflex.Attributes;
using SpaceTraveler.Scripts.Managers;
using UnityEngine;

namespace SpaceTraveler.Scripts.Infrastructure
{
    public class InitialSceneInstaller : MonoBehaviour
    {
        [Inject] private LevelManager _levelManager;

        private void Start()
        {
            _ = _levelManager.Load(Constants.Levels.MainMenuID);
        }
    }
}