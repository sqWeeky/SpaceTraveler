using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infastracture
{
    public class InitialSceneInstaller : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }
}