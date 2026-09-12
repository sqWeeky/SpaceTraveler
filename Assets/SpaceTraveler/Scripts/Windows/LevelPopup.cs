using TMPro;
using UnityEngine;
using UnityEngine.UI;
using SpaceTraveler.Scripts.Players;
using SpaceTraveler.Scripts.StateMachine.States;

namespace SpaceTraveler.Scripts.Windows
{
    public class LevelPopup : BaseWindow
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _bestTime;
        [SerializeField] private Image[] _stars;
        [SerializeField] private PlayerLabel[] _leaders;
        
        private string _levelName;

        public void Init(string sceneName)
        {
            _levelName = sceneName;
            _title.text = sceneName;
            //TODO: получить данные для таблицы лидеров
            //TODO: обновить лучшее время игрока
            //TODO: обновление звезд
        }

        public void LoadLevel()
        {
            LevelManager.LoadLevel(_levelName); // Загружаем конкретный уровень
            //GameStateMachine.ChangeState<LoadingState>();
        }

        public override void CloseWindow()
        {
            UIManager.CloseWindow<LevelPopup>();
        }

        private void FillLeaders()
        {
            
        }
    }
}