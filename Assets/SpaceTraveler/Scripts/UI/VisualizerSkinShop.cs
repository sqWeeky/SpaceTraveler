using System;
using SpaceTraveler.Scripts.Configs.Skins;
using SpaceTraveler.Scripts.Infrastructure;
using UnityEngine;

namespace SpaceTraveler.Scripts.UI
{
    public class VisualizerSkinShop : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<string, GameObject> _parentSkinsColor;

        private DataShipConfig _currentSkin;
        private string _currentShipColorName;
        private Transform _newColorShip;
        private GameObject _shipPlacement;

        private void Start()
        {
            if (_shipPlacement == null)
                _shipPlacement = GameObject.Find(Constants.GameTags.ShipPlacement);
            //_shipPlacement = GameObject.FindWithTag(Constants.GameTags.ShipPlacement);
            
            Debug.LogError($"Placement - {_shipPlacement == null}");
        }

        public void VisualizeSkin(string nameSkin, string colorSkin = "Red_White")
        {
            if (_newColorShip != null)
                _newColorShip.gameObject.SetActive(false);

            if (!_parentSkinsColor.ContainsKey(nameSkin))
                return;

            var newShip = _parentSkinsColor[nameSkin];
            _newColorShip = newShip.transform.Find(colorSkin);

            if (_newColorShip != null)
                _newColorShip.gameObject.SetActive(true);
        }
    }
}