using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialSceneInstaller : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}