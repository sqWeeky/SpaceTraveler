using System.Runtime.InteropServices;
using Reflex.Attributes;
using Reflex.Extensions;
using Reflex.Injectors;
using SpaceTraveler.Scripts.Managers;
using SpaceTraveler.Scripts.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceTraveler.Scripts.MenuLevels
{
    public class LevelButton : MonoBehaviour
    {
        [Inject] private UIManager _uiManager;

        [SerializeField] private string _nameLevelScene;
        [SerializeField] private TextMeshProUGUI _nameText;

        // [Inject]
        // private void Inject(UIManager uiManager)
        // {
        //     Debug.LogError("Inject");
        //     UIManager = uiManager;
        // }

        private void Start()
        {
            //if (_uiManager == null) 
            GameObjectInjector.InjectSingle(gameObject, SceneManager.GetActiveScene().GetSceneContainer());
        }

        public void Init(string levelName)
        {
            _nameLevelScene = levelName;
            _nameText.text = levelName;
        }

        public void OpenLevel()
        {
            Debug.LogError($"UIManager - {_uiManager == null}");
            _uiManager.OpenWindow<LevelPopup>().Init(_nameLevelScene);
        }
    }
}