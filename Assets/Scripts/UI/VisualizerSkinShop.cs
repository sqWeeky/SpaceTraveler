using Configs;
using Infrastructure;
using Reflex.Attributes;
using Reflex.Core;
using Skins;
using UnityEngine;

namespace UI
{
    public class VisualizerSkinShop : MonoBehaviour
    {
        [SerializeField] private Transform _placement;
        
        [Inject] private GameConfig _config;

        private Skin _currentSkin;
        private string _currentShipName;

        public void InitVisualize()
        {
            Debug.LogError(_config == null);
            _currentSkin =
                Resources.Load<Skin>(
                    $"Player/{_config.PlayerData.ShipData.Name}/{_config.PlayerData.CurrentSkinShip.Name}");

            Instantiate(_currentSkin.gameObject, _placement);
        }

        public void VisualizeSkin(string nameSkin)
        {
            _currentShipName = _config.PlayerData.ShipData.Name;
            //_currentShipName = Container.ProjectContainer.Resolve<GameConfig>().PlayerData.ShipData.Name;
            _currentSkin = Resources.Load<Skin>($"Player/PlayerPrefab");

            Instantiate(_currentSkin.gameObject, _placement);
        }
    }
}