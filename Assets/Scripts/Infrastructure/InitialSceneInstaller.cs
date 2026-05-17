using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class InitialSceneInstaller : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }
}