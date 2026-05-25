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
        
        //[Inject] private GameConfig _config;

        private Skin _currentSkin;
        private string _currentShipName;

        public void InitVisualize(GameConfig config)
        {
            _currentSkin =
                Resources.Load<Skin>(
                    $"Player/SkinPrefabs/{config.PlayerData.ShipData.Name}/{config.PlayerData.CurrentSkinShip.Name}");

            Instantiate(_currentSkin.gameObject, _placement);
        }

        public void VisualizeSkin(string nameSkin,  string colorSkin = "Blue_Black")
        {
            _currentSkin = null;
            Debug.LogError(nameSkin);
            //_currentShipName = _config.PlayerData.ShipData.Name;
            //_currentShipName = Container.ProjectContainer.Resolve<GameConfig>().PlayerData.ShipData.Name;
            _currentSkin = Resources.Load<Skin>($"Player/SkinPrefabs/{nameSkin}/{colorSkin}");

            Instantiate(_currentSkin.gameObject, _placement);
        }
    }
}