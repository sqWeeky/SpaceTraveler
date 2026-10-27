using Reflex.Attributes;
using SpaceTraveler.Scripts.Managers;
using SpaceTraveler.Scripts.Windows;
using TMPro;
using UnityEngine;

namespace SpaceTraveler.Scripts.MenuLevels
{
    public class LevelButton : MonoBehaviour
    {
        [Inject] private UIManager _uiManager;

        [SerializeField] private string _nameLevelScene;
        [SerializeField] private TextMeshProUGUI _nameText;

        public void Init(string levelName)
        {
            _nameLevelScene = levelName;
            _nameText.text = levelName;
        }

        public void OpenLevel()
        {
            _uiManager.OpenWindow<LevelPopup>().Init(_nameLevelScene);
        }
    }
}