using System.Collections.Generic;
using Configs;
using Configs.Skins;
using Reflex.Attributes;
using Skins;
using TMPro;
using UI;
using UnityEngine;

namespace Managers
{
    public class SkinManager : BaseManager<SkinManager>
    {
         [Inject] private GameConfig _config;

        [SerializeField] private List<DataShipConfig> _ships;
        [SerializeField] private VisualizerSkinShop _visualizerSkinShop;
        [SerializeField] private TextMeshProUGUI _skinNameText;
        [SerializeField] private TextMeshProUGUI _speedStat;
        [SerializeField] private TextMeshProUGUI _healthStat;

        //private GameConfig _config;
        private Skin _currentSkin;
        private DataShipConfig _currentShip;

        // public void Init(GameConfig config)
        // {
        //     if (!_config) 
        //         _config = config;
        // }

        public void Activate()
        {
            _currentShip = _config.PlayerData.ShipData;
            _currentSkin = _config.PlayerData.CurrentSkinShip;
            _skinNameText.text = _currentSkin.Name;
            _speedStat.text = _currentShip.Speed.ToString();
            _healthStat.text = _currentShip.MaxHealth.ToString();
            _visualizerSkinShop.InitVisualize(_config);
        }

        public void ChangeSkin( string colorSkin)
        {
            _visualizerSkinShop.VisualizeSkin(_currentSkin.Name, colorSkin);
        }

        public void SwitchRight()
        {
        }

        public void SwitchLeft()
        {
        }
    }
}