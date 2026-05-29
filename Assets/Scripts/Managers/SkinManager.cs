using System.Collections.Generic;
using Configs;
using Configs.Skins;
using Infrastructure;
using Reflex.Attributes;
using Reflex.Extensions;
using Reflex.Injectors;
using Skins;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class SkinManager : BaseManager<SkinManager>
    {
        [Inject] private GameConfig _config;

        [Header("Main Settings")]
        [SerializeField] private VisualizerSkinShop _visualizerSkinShop;
        [SerializeField] private TextMeshProUGUI _skinNameText;
        [SerializeField] private TextMeshProUGUI _speedStat;
        [SerializeField] private TextMeshProUGUI _healthStat;

        [Header("Text")]
        [SerializeField] private string _statSpeedText;
        [SerializeField] private string _statHealthText;

        [Header("Buttons")]
        [SerializeField] private Button _rightButton;
        [SerializeField] private Button _leftButton;
        
        private Skin _currentSkin;
        private DataShipConfig _currentShip;
        private int _index;

        // public void Activate()
        // {
        //     GameObjectInjector.InjectSingle(gameObject, SceneManager.GetActiveScene().GetSceneContainer());
        //
        //     _currentShip = _config.PlayerData.ShipData;
        //     _currentSkin = _config.PlayerData.CurrentSkinShip;
        //
        //     UpdateInformation();
        //
        //     _visualizerSkinShop.VisualizeSkin(_currentShip.Name, _currentSkin.ColorName);
        //
        //     _rightButton.onClick.AddListener(SwitchRight);
        //     _leftButton.onClick.AddListener(SwitchLeft);
        //
        //     _index = 0;
        // }

        private void OnEnable()
        {
            GameObjectInjector.InjectSingle(gameObject, SceneManager.GetActiveScene().GetSceneContainer());
        
            _currentShip = _config.PlayerData.ShipData;
            _currentSkin = _config.PlayerData.CurrentSkinShip;
        
            UpdateInformation();
        
            _visualizerSkinShop.VisualizeSkin(_currentShip.Name, _currentSkin.ColorName);
        
            _rightButton.onClick.AddListener(SwitchRight);
            _leftButton.onClick.AddListener(SwitchLeft);
        
            _index = 0;
        }

        private void OnDisable()
        {
            _rightButton.onClick.RemoveListener(SwitchRight);
            _leftButton.onClick.RemoveListener(SwitchLeft);
        }

        public void ChangeSkin(string colorSkin)
        {
            _visualizerSkinShop.VisualizeSkin(_currentShip.Name, colorSkin);
        }

        private void UpdateInformation()
        {
            _skinNameText.text = _currentShip.Name;
            _speedStat.text = $"{_statSpeedText}{_currentShip.Speed.ToString()}";
            _healthStat.text = $"{_statHealthText}{_currentShip.MaxHealth.ToString()}";
        }

        private void SwitchRight()
        {
            _index++;
            
            if (_index >= _config.ShipConfigs.Count)
            {
                _index = _config.ShipConfigs.Count - 1;
                return;
            }

            _currentShip = _config.ShipConfigs.Keys[_index];
            UpdateInformation();
            _visualizerSkinShop.VisualizeSkin(_currentShip.Name);
        }

        private void SwitchLeft()
        {
            _index--;

            if (_index < 0)
            {
                _index = 0;
                return;
            }

            _currentShip = _config.ShipConfigs.Keys[_index];
            UpdateInformation();
            _visualizerSkinShop.VisualizeSkin(_currentShip.Name);
        }
    }
}