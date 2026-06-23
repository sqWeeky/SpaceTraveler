using SpaceTraveler.Scripts.Configs.Skins;
using SpaceTraveler.Scripts.Skins;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpaceTraveler.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Configs/" + nameof(PlayerConfig))]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private DataShipConfig _defaultDataShip;
        [SerializeField] private Skin _defaultSkinShip;

        public DataShipConfig DefaultDataShip => _defaultDataShip;
        public Skin DefaultSkinShip => _defaultSkinShip;
    }
}