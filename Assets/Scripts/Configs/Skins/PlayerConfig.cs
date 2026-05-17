using System.Collections.Generic;
using Skins;
using UnityEngine;

namespace Configs.Skins
{
    [CreateAssetMenu(menuName = "Configs/" + nameof(PlayerConfig))]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private DataShipConfig _shipData;
        [SerializeField] private Skin _currentSkinShip;

        public DataShipConfig ShipData => _shipData;
        public Skin CurrentSkinShip => _currentSkinShip;
    }
}
