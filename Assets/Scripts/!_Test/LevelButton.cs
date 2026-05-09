using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace __Test
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private string _nameLevelScene;
        [SerializeField] private TextMeshProUGUI _nameText;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(LoadLevel);
        }

        public void Init(string levelName)
        {
            _nameLevelScene = levelName;
            _nameText.text = levelName;
        }

        private void LoadLevel()
        {
            SceneManager.LoadScene(_nameLevelScene);
        }
    }
}
