using Reflex.Attributes;
using Reflex.Extensions;
using Reflex.Injectors;
using SpaceTraveler.Scripts.Configs;
using SpaceTraveler.Scripts.Configs.Skins;
using SpaceTraveler.Scripts.Save;
using SpaceTraveler.Scripts.Skins;
using SpaceTraveler.Scripts.UI;
using SpaceTraveler.Scripts.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceTraveler.Scripts.Managers
{
    public class SkinManager : BaseManager<SkinManager>
    {
        [Inject] private GameConfig _config;
        [Inject] private SaveManager _saveManager;

        [Header("Main Settings")]
        [SerializeField] private VisualizerSkinShop _visualizerSkinShop;
        [SerializeField] private ShopWindow _shopWindow;

        private Skin _currentSkin;
        private DataShipConfig _currentShip;
        private int _index;
        private GameSaveData _data;

        private void OnEnable()
        {
            GameObjectInjector.InjectSingle(gameObject, SceneManager.GetActiveScene().GetSceneContainer());

            _data = _saveManager.Load();
            _currentShip = _data.PlayerData.CurrentShip;
            _currentSkin = _data.PlayerData.CurrentColorShip;
            _index = 0;
            _shopWindow.UpdateInformation(_currentShip, _data);
            _visualizerSkinShop.VisualizeSkin(_currentShip.Name, _currentSkin.ColorName);
            _shopWindow.ActivationPopup(false);

            if (_currentShip != null && _data.PlayerData.UnlockedPlayerShips.Contains(_currentShip))
                if (_data.PlayerData.CurrentShip == _currentShip)
                    _shopWindow.SwitchButtons(false, false);
                else
                    _shopWindow.SwitchButtons(false, true);

            foreach (DataShipConfig key in _config.ShipConfigs.Keys)
            {
                if (key != _currentShip)
                    _index++;
                else
                    break;
            }
        }

        public void ChangeColorSkin(string colorSkin)
        {
            _visualizerSkinShop.VisualizeSkin(_currentShip.Name, colorSkin);
            _saveManager.SaveColorShip(_currentShip, colorSkin);
        }

        public void ByeAndSelectShip(bool isNewShip)
        {
            if (isNewShip)
            {
                if (_data.PlayerData.PlayerStarScore >= _currentShip.Price)
                {
                    if (!_data.PlayerData.UnlockedPlayerShips.Contains(_currentShip))
                    {
                        _data.PlayerData.PlayerStarScore -= _currentShip.Price;
                        _saveManager.Save(_currentShip, true);
                        _shopWindow.SwitchButtons(false, false);
                        _shopWindow.UpdateInformation(_currentShip, _data);
                    }
                }
                else
                {
                    _shopWindow.ActivationPopup(true);
                }
            }
            else
            {
                if (_data.PlayerData.UnlockedPlayerShips.Contains(_currentShip))
                {
                    _saveManager.Save(_currentShip, false);
                }
            }
        }

        public void SwitchRight()
        {
            _index++;

            if (_index >= _config.ShipConfigs.Count)
            {
                _index = _config.ShipConfigs.Count - 1;
                return;
            }

            Switch();
        }

        public void SwitchLeft()
        {
            _index--;

            if (_index < 0)
            {
                _index = 0;
                return;
            }

            Switch();
        }
        
        private void Switch()
        {
            _currentShip = _config.ShipConfigs.Keys[_index];

            if (_currentShip != null && _data.PlayerData.UnlockedPlayerShips.Contains(_currentShip))
            {
                if (_data.PlayerData.CurrentShip == _currentShip)
                    _shopWindow.SwitchButtons(false, false);
                else
                    _shopWindow.SwitchButtons(false, true);
            }
            else
            {
                _shopWindow.SwitchButtons(true, false);
            }

            _shopWindow.UpdateInformation(_currentShip, _data);

            if (_currentShip != null && _data.PlayerData.UnlockedPlayerShips.Contains(_currentShip))
                _visualizerSkinShop.VisualizeSkin(_currentShip.Name, _data.PlayerData.CurrentColorShip.ColorName);
            else
                _visualizerSkinShop.VisualizeSkin(_currentShip.Name);
        }
    }
}