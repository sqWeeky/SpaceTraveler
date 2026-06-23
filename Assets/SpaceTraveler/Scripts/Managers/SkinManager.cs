using Reflex.Attributes;
using Reflex.Extensions;
using Reflex.Injectors;
using SpaceTraveler.Scripts.Configs;
using SpaceTraveler.Scripts.Configs.Skins;
using SpaceTraveler.Scripts.Skins;
using SpaceTraveler.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceTraveler.Scripts.Managers
{
    public class SkinManager : BaseManager<SkinManager>
    {
        [Inject] private GameConfig _config;
        [Inject] private SaveManager _saveManager;

        [Header("Main Settings")]
        [SerializeField] private VisualizerSkinShop _visualizerSkinShop;
        [SerializeField] private TextMeshProUGUI _skinNameText;
        [SerializeField] private TextMeshProUGUI _speedStat;
        [SerializeField] private TextMeshProUGUI _healthStat;
        [SerializeField] private TextMeshProUGUI _priceStat;

        [Header("Text")]
        [SerializeField] private string _statSpeedText;
        [SerializeField] private string _statHealthText;
        [SerializeField] private string _priceText;

        [Header("Buttons")]
        [SerializeField] private Button _rightButton;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _byeButton;
        [SerializeField] private Button _selectButton;

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

            _currentShip = _config.PlayerConfig.DefaultDataShip;
            _currentSkin = _config.PlayerConfig.DefaultSkinShip;

            _rightButton.onClick.AddListener(SwitchRight);
            _leftButton.onClick.AddListener(SwitchLeft);
            _byeButton.onClick.AddListener(ByeSkin);
            _selectButton.onClick.AddListener(SelectSkin);

            UpdateInformation();

            _visualizerSkinShop.VisualizeSkin(_currentShip.Name, _currentSkin.ColorName);
            SwitchButtons(false, true);
            
            _index = 0;
        }

        private void OnDisable()
        {
            _rightButton.onClick.RemoveListener(SwitchRight);
            _leftButton.onClick.RemoveListener(SwitchLeft);
            _byeButton.onClick.RemoveListener(ByeSkin);
            _selectButton.onClick.RemoveListener(SelectSkin);
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
            _priceStat.text = $"{_priceText}{_currentShip.Price.ToString()}";
        }

        private void ByeSkin()
        {
            
        }

        private void SelectSkin()
        {
            
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
            
            if (_currentShip != null && _currentShip.IsBuy)
                SwitchButtons(false, true);
            else
                SwitchButtons(true, false);

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
            
            if (_currentShip != null && _currentShip.IsBuy)
                SwitchButtons(false, true);
            else
                SwitchButtons(true, false);
            
            UpdateInformation();
            _visualizerSkinShop.VisualizeSkin(_currentShip.Name);
        }
        
        private void SwitchButtons(bool isBye, bool isSelect)
        {
            _byeButton.gameObject.SetActive(isBye);
            _selectButton.gameObject.SetActive(isSelect);
        }
    }
}