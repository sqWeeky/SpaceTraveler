using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceTraveler.Scripts.Infrastructure
{
    public class InitialSceneInstaller : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }
}