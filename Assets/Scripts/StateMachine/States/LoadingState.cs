using Windows;
using Cysharp.Threading.Tasks;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StateMachine.States
{
    public class LoadingState : GameState
    {
        private bool _isLoading;
        private string _sceneToLoad;

       public override void Enter()
        {
            Debug.Log("Entered Loading State");
            _isLoading = true;

            UIManager.CloseAllWindows();
            UIManager.OpenWindow<LoadingWindow>();

            LoadSceneAsync().Forget();
        }

        public override void Exit()
        {
            _isLoading = false;
            UIManager.CloseWindow<LoadingWindow>();
            Debug.Log("Exited Loading State");
        }

        private async UniTaskVoid LoadSceneAsync()
        {
            await UniTask.NextFrame();

            string sceneToLoad = LevelManager.GetSceneToLoad();
            Debug.Log($"LoadingState: Loading scene {sceneToLoad}");

            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneToLoad);

            if (loadOperation != null)
            {
                loadOperation.allowSceneActivation = false;

                await UniTask.Delay(1200);

                loadOperation.allowSceneActivation = true;
                await UniTask.WaitUntil(() => loadOperation.isDone);

                Debug.Log($"LoadingState: Scene {sceneToLoad} loaded successfully");
            }

            if (_isLoading)
            {
                DetermineNextState(sceneToLoad);
            }
        }

        private void DetermineNextState(string loadedScene)
        {
            if (loadedScene.Contains("Level") || loadedScene.Contains("Game"))
            {
                ChangeState<PlayingState>();
            }
            else
            {
                ChangeState<MenuState>();
            }
        }
    }
}